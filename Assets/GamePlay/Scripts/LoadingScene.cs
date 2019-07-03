using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene instance;

	private AsyncOperation currentLoadingOperation;
	private bool isLoading;
	public Slider sliderObj;
	public Text progressValueText;
    private const float minShowTime = 1f;
    private float timeElapsed;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance)
            Destroy(gameObject);

        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLoading)
        {
        	SetProgress(currentLoadingOperation.progress);
        	if(currentLoadingOperation.isDone)
        	{
        		Hide();
        	}
            else
            {
                timeElapsed += Time.deltaTime;
                if(timeElapsed >= minShowTime)
                    currentLoadingOperation.allowSceneActivation = true;
            }
        }
    }

    private void SetProgress(float p)
    {
    	sliderObj.value = p;
    	progressValueText.text = Mathf.CeilToInt(p * 100).ToString() + "%";
    }

    public void Show(AsyncOperation loadingOperation)
    {
    	gameObject.SetActive(true);
    	currentLoadingOperation = loadingOperation;
    	SetProgress(0f);
        timeElapsed = 0f;
    	isLoading = true;
    }

    public void Hide()
    {
    	gameObject.SetActive(false);
    	currentLoadingOperation = null;
    	isLoading = false;
    }
}
