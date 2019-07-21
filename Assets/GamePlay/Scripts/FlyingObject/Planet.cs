using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planet : MonoBehaviour
{

	void Start()
	{
		PlayPlanetAnim();
	}

	public void PlayPlanetAnim()
	{
		Vector3 rot = new Vector3(0f, 0f, 60f * Random.Range(0, 2));
		this.transform.DOLocalRotate(rot, 30f)
			.SetLoops(-1, LoopType.Incremental)
			.SetEase(Ease.Linear);
	}

	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
