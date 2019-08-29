using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Word : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayWordAnim();
    }

    public void PlayWordAnim()
    {
    	SpriteRenderer wordRenderer = GetComponent<SpriteRenderer>();
    	wordRenderer.DOFade(0, 0.8f)
    		.SetEase(Ease.InBack, 1.2f)
    		.OnComplete(RemoveWord);
    	this.transform.DOScale(1f, 0.8f)
    		.SetEase(Ease.InBack, 1.2f);
    }

    void RemoveWord()
    {
    	Destroy(this.gameObject);
    }
}
