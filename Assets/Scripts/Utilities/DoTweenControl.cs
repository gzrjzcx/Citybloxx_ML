using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenControl : MonoBehaviour
{
	// public Vector3 _pos;
	// public Vector3 targetJump;
	// public Vector3 targetRotation;
	void Start()
	{
		// FallenAnimation(1);
	}

	public void FallenAnimation(int fallenSide)
	{
		Vector3 targetJump = GameControl.instance.seaLevel;
		 		targetJump -= new Vector3(-4 * fallenSide, 0, 0);
		Vector3 targetRotation = new Vector3(0, -270 * fallenSide, 35);

		transform.DORotate(targetRotation, 2);
		transform.DOJump(targetJump, 2f, 1, 2).SetEase(Ease.Linear).OnComplete(OnJumpComplete);
	}

	private void OnJumpComplete()
	{
		transform.rotation = Quaternion.identity;
	}

	public void StackingNoDeadCenterAnimation()
	{
		Vector3 pos = GameControl.instance.columnObj.topPieceCollider.transform.localPosition;
		pos.x -= 0.5f;
		pos.y += 0.5f;
		// Debug.Log(GameControl.instance.columnObj.topPieceCollider.transform.localPosition);
		var go = new GameObject();
		go.transform.position = Vector3.zero;
		go.transform.SetParent(GameControl.instance.columnObj.transform, false);
		go.transform.localPosition = pos;
		transform.SetParent(go.transform, false);
		transform.localPosition = new Vector3(0, 0.5f, 0);
		go.transform.DOPunchRotation(new Vector3(0,0,45), 2f, 0, 0.5f);
		// transform.DOPunchRotation(pos, 2f, 0, 0.5f);
		// transform.DOPunchPosition(new Vector3(-0.5f, -0.5f, 0), 2f, 0, 0.5f).SetEase(Ease.OutExpo);
	}
}
