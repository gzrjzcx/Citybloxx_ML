using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarControl : MonoBehaviour
{
	private List<string> starName;
	private List<GameObject> starList;
	private GameObject starPrefab;
	private GameObject star;

	void Update()
	{
		if(Input.GetKey(KeyCode.S))
		{
			SpawnStar();
		}
	}

	void Start()
	{
		starList = new List<GameObject>();
		starName = new List<string>();
		for(int i=0; i<=8; i++)
			starName.Add("star_" + i.ToString());
	}


	public void SpawnMultiStar()
	{
		if(GameControl.instance.stackedPieceNum > 20)
		{
			for(int i=0; i<Random.Range(3, 6); i++)
			{
				SpawnStar();
			}
		}
	}

	private void SpawnStar()
	{
		string pfbPath = "star/prefabs/Star";
		string imgPath = "star/images/clear_";
		starPrefab = Resources.Load<GameObject>(pfbPath);
		Sprite img = Resources.Load<Sprite>(imgPath + Random.Range(0, 9));

		Vector3 leftTopPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, Screen.height, 8));

		float offsetY = 5f;

		Vector3 spawnPos = new Vector3(
			Random.Range(leftTopPos.x, -leftTopPos.x),
			Random.Range(leftTopPos.y, leftTopPos.y + offsetY), 0f);
		Vector3 rotEuler = new Vector3(0, 0, Random.Range(0f, 360f));
		float scale = Random.Range(1f, 2f);

		star = Instantiate(starPrefab, spawnPos, Quaternion.Euler(rotEuler));
		starList.Add(star);
		star.transform.SetParent(this.transform, true);
		star.GetComponent<SpriteRenderer>().sprite = img;
		star.transform.localScale = new Vector3(scale, scale, 1);

	}
}
