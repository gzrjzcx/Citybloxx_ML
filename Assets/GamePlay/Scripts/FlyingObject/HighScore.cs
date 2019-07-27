using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HighScore : MonoBehaviour
{

	public TextMeshPro nameText;

    // Start is called before the first frame update
    void Start()
    {
        PlayHighScoreAnim();
    }

    void PlayHighScoreAnim()
    {
    	Sequence highScoreAnimSeq = DOTween.Sequence()
    									.SetId("highScoreAnimSeq")
    									.SetLink(this.gameObject);

    	float y = GameControl.instance.columnPiecesObj.topPiece.transform.position.y;
    	Tween mov_in = this.transform.DOLocalMoveY(y, 1f)
    		.SetEase(Ease.OutBack);
        Tween suspend = this.transform.DOLocalMoveY(y + 0.2f, 1f)
            .SetLoops(10, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);

        highScoreAnimSeq.Append(mov_in);
        highScoreAnimSeq.Append(suspend);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }   
}
