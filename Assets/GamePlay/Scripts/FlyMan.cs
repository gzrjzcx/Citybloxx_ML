using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyMan : MonoBehaviour
{
 	void OnTriggerEnter2D()
 	{
 		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
 		renderer.DOFade(0, 0.3f).OnComplete(DestroyGO);
 	}

 	void DestroyGO()
 	{
 		Destroy(this.gameObject);
 	}
}
