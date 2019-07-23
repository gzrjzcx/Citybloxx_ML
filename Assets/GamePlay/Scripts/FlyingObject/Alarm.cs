using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Alarm : MonoBehaviour
{

	public GameObject pressImg;

	void Update()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			Time.timeScale = 1f;
			Destroy(this.gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		PlayAlarmAnim();        
    }

    public void PlayAlarmAnim()
    {
    	SpriteRenderer  pressRenderer = pressImg.GetComponent<SpriteRenderer>();
    	pressImg.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f, 1, 0)
    		.SetLoops(-1, LoopType.Restart)
    		.timeScale = 10f;
    	pressRenderer.DOColor(new Color(60, 60, 60), 0.3f)
    		.SetEase(Ease.Flash, 2, 0)
    		.SetLoops(-1, LoopType.Restart)
    		.timeScale = 10f;
    }
}
