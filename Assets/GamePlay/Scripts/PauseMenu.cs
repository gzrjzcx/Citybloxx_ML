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
    public Button music;
    public Button sound;


    private string path = "pauseMenu/button/";

    public void Resume()
    {
    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
        AudioControl.instance.Play("Click");
    }

    public void Pause()
    {
    	pauseMenuUI.SetActive(true);
    	Time.timeScale = 0f;
        AudioControl.instance.Play("Click");

    }

    public void Restart()
    {
    	SceneManager.LoadSceneAsync("Main");
        AudioControl.instance.Play("Click");

    }

    public void Exit()
    {
    	Application.Quit();
        AudioControl.instance.Play("Click");

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
        AudioControl.instance.Play("Click");

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
        AudioControl.instance.Play("Click");

    }

    public void ToggleMusic()
    {
        if(AudioControl.instance.isPlaying("Theme"))
        {
            AudioControl.instance.Stop("Theme");
            Sprite img = Resources.Load<Sprite>(path + "music_disable");
            music.GetComponent<Image>().sprite = img;
        }
        else
        {
            AudioControl.instance.Play("Theme");
            Sprite img = Resources.Load<Sprite>(path + "music_enable");
            music.GetComponent<Image>().sprite = img;
        }
        AudioControl.instance.Play("Click");

    }

    public void ToggleSound()
    {
        if(AudioControl.instance.isMute("Block_Hit"))
        {
            AudioControl.instance.UnmuteAllSoundEffects();
            Sprite img = Resources.Load<Sprite>(path + "sound_enable");
            sound.GetComponent<Image>().sprite = img;
        }
        else
        {
            AudioControl.instance.MuteAllSoundEffects();
            Sprite img = Resources.Load<Sprite>(path + "sound_disable");
            sound.GetComponent<Image>().sprite = img;
        }
        AudioControl.instance.Play("Click");

    }




























}
