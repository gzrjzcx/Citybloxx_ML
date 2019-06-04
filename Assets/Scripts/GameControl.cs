using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;

	public PiecePool piecePoolObj;
    public ColumnSwinging columnObj;
    public ScreenMoveUp screenMoveUpObj;
    public EllipticalOrbit slingObj;
    public StackingEffect stackingEffectObj;

    public Text scoreText;
    public Text missText;
    public Text comboNumText;
    public Text comboScoreText;
    public GameObject gameOverText;
    public GameObject columnGameObj;
    public Slider comboSlider;
    public Timer comboTimer;

    public bool isFirstPieceStacked = false;
    public bool isGameOver = false;

    public int populationScore = 0;
    public int stackedPieceNum = 0;
    public int missNum = 0;
    public int comboNum = 0;

	// public bool 

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
        comboTimer = Timer.createTimer("ComboTimer");
    }

    void Update()
    {
        if(isGameOver && Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnPieceStacking()
    {
        PieceStacked();
    }

    public void AfterPieceStackingSuccessfully(bool isDeadCenter)
    {
        CheckFirstPieceIfStacked();
        Scored();
        ScreenMoveUp();
        if(isDeadCenter)
            Combo();
    }

    public void AfterPieceStackingFailed(int fallenSide)
    {
        Missed();
        CheckMissNum();
    }

    void PieceStacked()
    {
    	piecePoolObj.HookNewPiece();
    }

    void Scored()
    {
        stackedPieceNum++;
        scoreText.text = "Score:" + stackedPieceNum.ToString();
    }

    void Missed()
    {
        missNum++;
        missText.text = "Miss:" + missNum.ToString();
    }

    void CheckMissNum()
    {
        if(missNum > 2)
        {
            isGameOver = true;
            gameOverText.SetActive(true);
        }
    }

    void ScreenMoveUp()
    {
        screenMoveUpObj.MoveUp();
    }

    public void Combo()
    {
        comboNum++;
        comboNumText.text = "X " + comboNum.ToString();
        comboSlider.gameObject.SetActive(true);
        if(comboTimer.isTiming)
        {
            comboTimer.RestartTimerForCombo();
        }
        else
            comboTimer.startTiming(5, OnComboTimingComplete, OnComboTimingProcess, true, false, false);
    }

    void OnComboTimingComplete()
    {
        comboSlider.gameObject.SetActive(false);
        comboScored();
        comboNum = 0;
    }

    void OnComboTimingProcess(float p)
    {
        comboSlider.value = 1 - p;
        //Debug.Log("combo timing process" + p);
    }

    void comboScored()
    {
        comboScoreText.gameObject.SetActive(true);
        int comboScore = comboNum * 3;
        comboScoreText.text = "+ " + comboScore.ToString(); 
        stackedPieceNum += comboScore;
        scoreText.text = "Score:" + stackedPieceNum.ToString();
        Invoke("delayInactiveComboScoreText", 2);
    }

    void delayInactiveComboScoreText()
    {
        comboScoreText.gameObject.SetActive(false);
    }

    void CheckFirstPieceIfStacked()
    {
        if(!isFirstPieceStacked)
        {
            isFirstPieceStacked = true;
        }
    }


















}
