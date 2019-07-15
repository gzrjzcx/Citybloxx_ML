﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public GameObject pauseMenuUI;
    public RectTransform pauseWindow;

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
}