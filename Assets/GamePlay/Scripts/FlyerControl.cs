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
	private GameObject flyman;

	[SerializeField]
	private int index=2;

	void Start()
	{
		manAnim = GetComponent<Animator>();
		flymanName = new List<string>();
		for(int i=1; i<=6; i++)
		{
			flymanName.Add("flyman" + i.ToString());
		}
	}

	public void SpawnFlyman()
	{
		if(GameControl.instance.stackedPieceNum < 2)
			return;
		string path = "flyer/man/flyman";
		// flymanPrefab = Resources.Load<GameObject>(path + Random.Range(1, 6));
		flymanPrefab = Resources.Load<GameObject>(path + index);
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
		flyman.transform.SetParent(this.transform, true);
		flyman.GetComponent<SpriteRenderer>().sortingLayerName = "Midground Layer";
		flyman.GetComponent<SpriteRenderer>().sortingOrder = 2;
		flyman.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		ManFly(sign);
	}

	private void ManFly(int sign)
	{
		float _x = 2 * sign;

		Sequence jumpAnimSeq = DOTween.Sequence();
		jumpAnimSeq.SetLink(flyman.gameObject);
		Tween run = flyman.transform.DOLocalMoveX(flyman.transform.localPosition.x - _x, 1f);

		float _y = GameControl.instance.columnPiecesObj.topPiece.transform.position.y;
		Vector3 pos = new Vector3(0 - sign*1, Random.Range(_y-3, _y), 0);
		// Debug.Log("pos = " + pos);
		Tween jump = flyman.transform.DOLocalJump(pos, 2f, 1, 2f);

		jumpAnimSeq.Append(run);
		jumpAnimSeq.Append(jump);
	}


}
