using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour
{

	public GameObject pauseMenuUI;

	public void PauseBtnClicked()
	{
		pauseMenuUI.SetActive(true);
		pauseMenuUI.GetComponent<PauseMenu>().Pause();
	}

}
