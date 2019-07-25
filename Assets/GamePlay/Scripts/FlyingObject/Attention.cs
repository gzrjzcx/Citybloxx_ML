using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attention : MonoBehaviour
{
	private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
		spawnPos = this.transform.localPosition;
		PlayAttentionAnim();     
    }

    void PlayAttentionAnim()
    {

    	Sequence attentionAnimSeq = DOTween.Sequence()
    		.SetId("attentionAnimSeq")
    		.SetLink(this.gameObject);

    	Tween mov_in = this.transform.DOLocalMoveX(spawnPos.x + 4.2f, 1f)
    		.SetEase(Ease.OutBack);
    	Tween scale = this.transform.DOPunchScale(
    		new Vector3(0.1f, 0.1f, 0.1f), 1f, 6, 1);
    	Tween mov_out = this.transform.DOLocalMoveX(spawnPos.x, 1f)
    		.SetEase(Ease.InBack);

    	attentionAnimSeq.Append(mov_in);
    	attentionAnimSeq.Append(scale);
    	attentionAnimSeq.Append(mov_out);
    }

	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
