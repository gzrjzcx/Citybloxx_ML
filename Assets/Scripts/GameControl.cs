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
    public GameObject gameOverText;
    public GameObject columnGameObj;

    public bool isFirstPieceStacked = false;
    public bool isGameOver = false;

    public int populationScore = 0;
    public int stackedPieceNum = 0;
    public int missNum = 0;

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

    public void AfterPieceStackingSuccessfully()
    {
        CheckFirstPieceIfStacked();
        Scored();
        ScreenMoveUp();
    }

    public void AfterPieceStackingFailed()
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

    void CheckFirstPieceIfStacked()
    {
        Debug.Log(columnGameObj.activeSelf);
        if(!isFirstPieceStacked)
        {
            Debug.Log(columnGameObj.activeSelf);
            isFirstPieceStacked = true;
        }
    }

    public int getStackedPiecesNum()
    {
        return stackedPieceNum;
    }
}
