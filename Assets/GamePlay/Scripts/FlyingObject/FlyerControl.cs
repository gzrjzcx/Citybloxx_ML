using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyerControl : MonoBehaviour
{
	private Animator manAnim;
	[SerializeField]
	private List<string> flymanName;
	private GameObject flymanPrefab;
	public List<GameObject> flymanList;
	private GameObject flyman;

	// [SerializeField]
	// private int index=1;

	[SerializeField]
	private int spawnThreshold;

	// void Update()
	// {
	// 	if(Input.GetKey(KeyCode.K))
	// 	{
	// 		KillAllFlyMan();
	// 	}
	// 	if(Input.GetKey(KeyCode.F))
	// 	{
	// 		SpawnFlyman();
	// 	}
	// }

	void Start()
	{
		manAnim = GetComponent<Animator>();
		flymanName = new List<string>();
		flymanList = new List<GameObject>();
		for(int i=1; i<=6; i++)
		{
			flymanName.Add("flyman" + i.ToString());
		}
	}

	public void SpawnMultiFlyman()
	{
		if(GameControl.instance.stackedPieceNum < spawnThreshold)
			return;

		for(int i=0; i<Random.Range(1, 4); i++)
		{
			Invoke("SpawnFlyman", Random.Range(0.05f, 0.2f));
		}
	}

	public void SpawnFlyman()
	{
		string path = "flyer/man/flyman";
		flymanPrefab = Resources.Load<GameObject>(path + Random.Range(1, 7));
		// flymanPrefab = Resources.Load<GameObject>(path + index);
		System.Random rnd = new System.Random();
		int sign = (rnd.Next(2) == 1) ? 1 : -1; // 1->right
		float offsetX = 0.5f * sign;
		float offsetY = 1.5f;		
		float _x = (sign < 0) ? 0 : Screen.width;
		float rot = (sign < 0) ? 0 : 180f;
		Vector3 boundary_pos = Camera.main.ScreenToWorldPoint(new Vector3(_x,0,8));
		// Debug.Log(sign + " " + boundary_pos);

		Vector3 spawn_pos = new Vector3(boundary_pos.x - offsetX, 
										boundary_pos.y + offsetY, 0);
		// Debug.Log("spawn " + spawn_pos);
		flyman = Instantiate(flymanPrefab, spawn_pos, Quaternion.Euler(0, rot, 0));
		flymanList.Add(flyman);
		flyman.transform.SetParent(this.transform, true);
		flyman.GetComponent<SpriteRenderer>().sortingLayerName = "Midground Layer";
		flyman.GetComponent<SpriteRenderer>().sortingOrder = 2;
		flyman.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		ManFly(sign);
	}

	public void KillAllFlyMan()
	{
		if(flymanList.Count > 0)
		{
			for(int i=0; i<flymanList.Count; i++)
			{
				Animator anim = flymanList[i].GetComponent<Animator>();
				// if animator is playing any animation
				// if((anim.GetCurrentAnimatorStateInfo(0).length > 
				// 	anim.GetCurrentAnimatorStateInfo(0).normalizedTime) &&
				// 	!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Die"))
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Base.Die"))
				{
					anim.SetBool("Die", true);
					DOTween.Kill("FlyManSeq");
				}
			}
		}
	}

	private void ManFly(int sign)
	{
		float _x = 2 * sign;

		Sequence jumpAnimSeq = DOTween.Sequence().SetId("FlyManSeq");
		jumpAnimSeq.SetLink(flyman.gameObject);
		Tween run = flyman.transform.DOLocalMoveX(flyman.transform.localPosition.x - _x, 1f);

		float _y = GameControl.instance.columnPiecesObj.topPiece.transform.position.y;
		Vector3 pos = new Vector3(0 - sign*1, Random.Range(_y-3, _y), 0);
		Tween jump = flyman.transform.DOLocalJump(pos, 2f, 1, 2f);

		jumpAnimSeq.Append(run);
		jumpAnimSeq.Append(jump);
	}


}
