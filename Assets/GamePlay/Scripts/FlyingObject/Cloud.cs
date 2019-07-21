using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cloud : MonoBehaviour
{

	public void PlayCloudAnim(int sign, Vector3 leftTopPos)
	{
		float endValue = leftTopPos.x * sign;
		float imgOffset = -2f * sign;
		Debug.Log(endValue, gameObject);

		Sequence cloudAnimSeq = DOTween.Sequence().SetId("cloudAnimSeq");
		cloudAnimSeq.SetLink(this.gameObject);
		Tween mov = this.transform.DOLocalMoveX(endValue + imgOffset, Random.Range(30f, 60f))
									.SetEase(Ease.Linear);
		cloudAnimSeq.Append(mov);
		cloudAnimSeq.AppendCallback(RemoveCloud);

	}

	void RemoveCloud()
	{
		GameControl.instance.cloudObj.cloudList.Remove(this.gameObject);
		Destroy(this.gameObject);
	}

	void OnBecameInvisible()
	{
		RemoveCloud();
	}
}
