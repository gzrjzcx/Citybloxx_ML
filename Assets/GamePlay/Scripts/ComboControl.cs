using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ComboControl : MonoBehaviour
{
    
	public Slider comboSlider;
    public Timer comboTimer;
    public TextMeshProUGUI comboNumText;
    public TextMeshProUGUI comboScoreText;
    public Image comboBarStar;
    public Image fillImage;
    
    public int comboNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        comboTimer = Timer.createTimer("ComboTimer");       
    }

    public void Combo()
    {
        AddComboNum();
        comboSlider.gameObject.SetActive(true);
        if(comboTimer.isTiming)
        {
            comboTimer.RestartTimerForCombo();
        }
        else
        {
            comboTimer.startTiming(5, OnComboTimingComplete, OnComboTimingProcess, false, false, false);
            PlayComboBarStarAnim();
        }

        FlashComboBar();
    }

    void PlayComboBarStarAnim()
    {
        Sequence barStarAnimSeq = DOTween.Sequence();
        barStarAnimSeq.SetLink(comboSlider.gameObject, LinkBehaviour.KillOnDisable);
        Tween rot = comboBarStar.transform.DOLocalRotate(new Vector3(0,0,60), 0.3f);
        Tween scale = comboBarStar.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0), 0.3f, 1, 0);
        barStarAnimSeq.Append(rot);
        barStarAnimSeq.Join(scale);
        barStarAnimSeq.SetLoops(-1, LoopType.Incremental);
        barStarAnimSeq.OnKill(resetBarStar);
    }

    void resetBarStar()
    {
        comboBarStar.transform.localScale = new Vector3(1,1,1);
        comboBarStar.transform.localRotation = Quaternion.identity;
    }

    void FlashComboBar()
    {
        fillImage.DOColor(Color.white, 0.08f).SetEase(Ease.Flash, 2, 0);
    }

    public void AddComboNum()
    {
        comboNum++;
        comboNumText.text = "x " + comboNum.ToString();
    }

    public void PlayComboSound()
    {
        switch(comboNum)
        {
            case 1:
                AudioControl.instance.Play("Good");
                GameControl.instance.wordObj.SpawnWord("Good");
                break;
            case 3:
                AudioControl.instance.Play("Great");
                GameControl.instance.wordObj.SpawnWord("Great");
                break;
            case 5: 
                AudioControl.instance.Play("Excellent");
                GameControl.instance.wordObj.SpawnWord("Excellent");
                break;
            case 8:
                AudioControl.instance.Play("Amazing");
                GameControl.instance.wordObj.SpawnWord("Amazing");
                break;
            case 10:
                AudioControl.instance.Play("Unbelievable");
                GameControl.instance.wordObj.SpawnWord("Unbelievable");
                break;
        }
    }

    void OnComboTimingComplete()
    {
        comboSlider.gameObject.SetActive(false);
        GameControl.instance.gameStatus = GameControl.GameStatus.GAME_RUNNING;
        comboScored();
        GameControl.instance.particleObj.StopComboPeriodAnim();
        comboNum = 0;
    }

    void OnComboTimingProcess(float p)
    {
        comboSlider.value = 1 - p;
    }

    public void comboScored()
    {
        comboScoreText.gameObject.SetActive(true);
        int comboScore = comboNum * 3;
        comboScoreText.text = "+ " + comboScore.ToString();
        AudioControl.instance.Play("Wow");
        GameControl.instance.populationScore += comboScore;
        GameControl.instance.populationScoreText.text = GameControl.instance.populationScore.ToString();
        Invoke("delayInactiveComboScoreText", 1);
    }

    void delayInactiveComboScoreText()
    {
        comboScoreText.gameObject.SetActive(false);
    }
}
