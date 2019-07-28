using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

	public GameObject pauseMenuUI;
    public RectTransform pauseWindow;

    public Button DDA;
    public Button ATS;

    private string path = "pauseMenu/button/";

    public void Resume()
    {
    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    }

    public void Pause()
    {
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
    }

    public void Restart()
    {
    	SceneManager.LoadSceneAsync("Main");
    }

    public void Exit()
    {
    	Application.Quit();
    }

    public void ToggleDDA()
    {
        if(GameControl.instance.aiObj.isDDA)
        {
            GameControl.instance.aiObj.isDDA = false;
            Sprite img = Resources.Load<Sprite>(path + "dda_disable");
            DDA.GetComponent<Image>().sprite = img;
        }
        else
        {
            GameControl.instance.aiObj.isDDA = true;
            Sprite img = Resources.Load<Sprite>(path + "dda_enable");
            DDA.GetComponent<Image>().sprite = img;
        }
    }

    public void ToggleATS()
    {
        if(GameControl.instance.aiObj.isATS)
        {
            GameControl.instance.aiObj.isATS = false;
            Sprite img = Resources.Load<Sprite>(path + "ats_disable");
            ATS.GetComponent<Image>().sprite = img;
        }
        else
        {
            GameControl.instance.aiObj.isATS = true;
            Sprite img = Resources.Load<Sprite>(path + "ats_enable");
            ATS.GetComponent<Image>().sprite = img;
        }
    }
}
