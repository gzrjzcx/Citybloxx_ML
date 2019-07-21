using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayStarAnim();
    }

    void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}

	public void PlayStarAnim()
	{
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		renderer.DOFade(Random.Range(0f, 20f), Random.Range(1f,2f))
			.SetLoops(-1, LoopType.Restart);

		float punchScale = Random.Range(1f, 3f);
		this.transform.DOScale(new Vector3(punchScale, punchScale, 1), 
			Random.Range(3f, 8f))
			.SetLoops(-1, LoopType.Restart);
	}
}
