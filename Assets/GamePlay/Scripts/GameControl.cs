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

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI populationScoreText;
    public GameObject gameOverText;
    public GameObject[] lifePieces;

    public int populationScore = 0;
    public int stackedPieceNum = 0;
    public int missNum = 0;
    public Vector3 seaLevel;

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
    }

    public void AfterCollisionAtGameStart()
    {
        if(gameStatus == GameStatus.GAME_START)
        {
            gameStatus = GameStatus.GAME_FIRSTPIECE;
        }
        screenMoveUpObj.ObstacleMoveUp();
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
        seaLevel.y++;

        if(isDeadCenter)
        {
            gameStatus = GameStatus.GAME_COMBO;
            comboControlObj.Combo();
            columnObj.Set2ComboSwingingAmplitude();
            particleObj.PlayComboPeriodAnim();
        }
        else if(gameStatus == GameStatus.GAME_COMBO)
        {
            comboControlObj.AddComboNum();            
        }
    }

    public void AfterPieceStackingFailed(int fallenSide)
    {
        Missed();
        CheckMissNum();
        screenMoveUpObj.ShakeCamera();
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

    // Use this to control the ideal distance
    // void LockDistanceColumn2Sling()
    // {
    //     Debug.Log(columnObj.GetDistanceColumn2Sling);
    //     if(!Mathf.Approximately(columnObj.initialDistance, 
    //         columnObj.GetDistanceColumn2Sling))
    //         {
    //             columnObj.transform.position = slingObj.transform.position 
    //                 - new Vector3(0, columnObj.initialDistance - slingObj.offsetY, 0);
    //         }  
    // }
















}
