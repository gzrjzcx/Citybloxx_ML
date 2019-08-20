using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GO_Extensions;
using DG.Tweening;
using TMPro;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;

	public PiecePool piecePoolObj;
    public ColumnSwinging columnObj;
    public ColumnPieces columnPiecesObj;
    public ScreenMoveUp screenMoveUpObj;
    public EllipticalOrbit slingObj;
    public ComboControl comboControlObj;
    public DoTweenControl doTweenObj;
    public ParticleControl particleObj;
    public MyCollisionControl mycolObj;
    public AIControl aiObj;
    public FlyerControl flyerObj;
    public CloudsControl cloudObj;
    public PlanetControl planetObj;
    public StarControl starObj;
    public SpaceFOControl foObj;
    public HighScoreControl highScoreObj;
    public Tester tester;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI populationScoreText;
    public GameObject gameOverText;
    public GameObject[] lifePieces;
    public InputField nameInputFieldObj;

    public int populationScore = 0;
    public int stackedPieceNum = 0;
    public int missNum = 0;
    public string playerName;
    public Vector3 seaLevel;

    public bool isDug;

    public enum GameStatus
    {
        GAME_READY = 0,  // Scene has been loaded, but game not start
        GAME_START, // Game start, but first piece not fallen
        GAME_FIRSTPIECE, // The first piece has fallen, but the second piece not fall
        GAME_RUNNING, // Game is running but not combo, the second piece has fallen
        GAME_COMBO, // Game is running and in combo phase
        GAME_OVER // Game is over.
    }
    public GameStatus gameStatus;

    public bool isGameRunning
    {
        get {return (gameStatus == GameStatus.GAME_RUNNING 
            || gameStatus == GameStatus.GAME_COMBO);}
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(!instance)
        {
        	instance = this;
        }else if(instance)
        {
        	Destroy(gameObject);
        }
    }

    void Start()
    {
        seaLevel = new Vector3(0, -10, 0);
        gameStatus = GameStatus.GAME_START;
    }

    void Update()
    {
        if(gameStatus == GameStatus.GAME_OVER 
            && (Input.GetKeyDown("space") || Input.touchCount > 0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameStatus = GameStatus.GAME_START; 
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            ScreenCapture.CaptureScreenshot("screenshot/" + Time.time + ".png");
        }
    }

    public void AfterCollisionAtGameStart()
    {
        if(gameStatus == GameStatus.GAME_START)
        {
            gameStatus = GameStatus.GAME_FIRSTPIECE;
        }
        screenMoveUpObj.ObstacleMoveUp();
        AudioControl.instance.Play("Block_Hit");
        piecePoolObj.HookNewPiece();
    }

    public void AfterCollisionAtFirstPiece()
    {
        if(gameStatus == GameStatus.GAME_FIRSTPIECE)
        {
            gameStatus = GameStatus.GAME_RUNNING;
        }
    }



    public void AfterPieceStackingSuccessfully(bool isDeadCenter)
    {
        CheckIfGameRunning();
        Scored();
        ScreenMoveUp();
        SetColumnSwinging();
        flyerObj.SpawnMultiFlyman();
        cloudObj.SpawnMultiClouds();
        planetObj.SpawnPlanet();
        starObj.SpawnMultiStar();
        foObj.SpawnFO();
        highScoreObj.ShowHighScore();
        aiObj.AddMinPosY();
        seaLevel.y++;

        if(isDeadCenter)
        {
            gameStatus = GameStatus.GAME_COMBO;
            comboControlObj.Combo();
            columnObj.Set2ComboSwingingAmplitude();
            particleObj.PlayComboPeriodAnim();
            tester.dcNum++;
        }
        else if(gameStatus == GameStatus.GAME_COMBO)
        {
            comboControlObj.AddComboNum();            
        }

        comboControlObj.PlayComboSound();
    }

    public void AfterPieceStackingFailed(int fallenSide)
    {
        if(tester.testType == Tester.TestType.NORMAL)
        {
            Missed();
            CheckMissNum();
            tester.Record();
        }
        tester.missNum++;
        screenMoveUpObj.ShakeCamera();
        flyerObj.KillAllFlyMan();
        GameControl.instance.aiObj.stackAgentObj.isPlaying = false;
    }

    void Scored()
    {
        stackedPieceNum++;
        populationScore++;
        scoreText.text = stackedPieceNum.ToString();
        populationScoreText.text = populationScore.ToString();
    }

    void Missed()
    {
        lifePieces[missNum].SetActive(false);
        missNum++;
    }

    void CheckMissNum()
    {
        if(missNum > 2)
        {
            gameOverText.SetActive(true);
            if(gameStatus == GameStatus.GAME_COMBO)
            {
                comboControlObj.comboTimer.EndTiming();
                comboControlObj.comboScored();
            }
            gameStatus = GameStatus.GAME_OVER;
            AudioControl.instance.Play("Round_End");
            AudioControl.instance.Stop("Theme");
        }
    }

    void ScreenMoveUp()
    {
        screenMoveUpObj.MoveUp();
    }

    void CheckIfGameRunning()
    {
        if(gameStatus == GameStatus.GAME_FIRSTPIECE)
        {
            gameStatus = GameStatus.GAME_RUNNING;
        }
    }

    void SetColumnSwinging()
    {
        columnObj.SwingingCenterMoveUp();
        columnObj.AddAmplitudeRotate();
        columnObj.SetAmplitudeIncrementAndMax();
    }

    public void SavePlayerName()
    {
        playerName = nameInputFieldObj.text;
        highScoreObj.UpdateHighScore(stackedPieceNum, playerName);
    }













}
