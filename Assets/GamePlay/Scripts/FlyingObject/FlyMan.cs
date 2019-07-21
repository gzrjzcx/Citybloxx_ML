using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyMan : MonoBehaviour
{
 	void OnTriggerEnter2D()
 	{
 		KillFlyMan();
 	}

 	void DestroyGO()
 	{
 		GameControl.instance.flyerObj.flymanList.Remove(this.gameObject);
 		Destroy(this.gameObject);
 	}

 	public void KillFlyMan()
 	{
 		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
 		renderer.DOFade(0, 0.3f).OnComplete(DestroyGO).SetId("FadeOut" + gameObject.name);
 	}
}
