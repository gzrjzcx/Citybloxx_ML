using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloudsControl : MonoBehaviour
{
	private List<string> cloudName;
	private GameObject cloudPrefab;
	private GameObject cloud;
	public List<GameObject> cloudList;
	[SerializeField]
	private int lastCloudPieceNum = 10;

	// void Update()
	// {
	// 	if(Input.GetKey(KeyCode.C))
	// 	{
	// 		SpawnCloud();
	// 	}
	// }

	void Start()
	{
		cloudList = new List<GameObject>();
		cloudName = new List<string>();
		for(int i=1; i<=23; i++)
		{
			cloudName.Add("clouds_" + i.ToString());
		}
	}

	public void SpawnMultiClouds()
	{
		if(GameControl.instance.stackedPieceNum <
			lastCloudPieceNum + 3 + Random.Range(5, 10))
			return;



		for(int i=0; i<Random.Range(1, 4); i++)
		{
			Invoke("SpawnCloud", Random.Range(0.05f, 0.2f));			
		}
		lastCloudPieceNum = GameControl.instance.stackedPieceNum;
	}

	public void SpawnCloud()
	{
		string pfbPath = "cloud/prefabs/cloud";
		string imgPath = "cloud/images/clouds_";
		cloudPrefab = Resources.Load<GameObject>(pfbPath);
		Sprite img = Resources.Load<Sprite>(imgPath + Random.Range(1, 24));

		System.Random rnd = new System.Random();
		int leftOfRightSign = (rnd.Next(2) == 1) ? 1 : -1; // 1->right

		Vector3 leftTopPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, Screen.height, 8));

		float width = Mathf.Abs(leftTopPos.x * 2);
		float offsetX = Random.Range(0f, width) * leftOfRightSign;
		float offsetY = Random.Range(1f, 3f);
		float _x = (leftOfRightSign == 1) ? -leftTopPos.x : leftTopPos.x;
		Vector3 spawn_pos = new Vector3(_x - offsetX, 
										leftTopPos.y + offsetY, 0f);

		cloud = Instantiate(cloudPrefab, spawn_pos, Quaternion.identity);
		cloud.GetComponent<SpriteRenderer>().sprite = img;
		cloudList.Add(cloud);
		cloud.transform.SetParent(this.transform, true);
		cloud.GetComponent<Cloud>().PlayCloudAnim(leftOfRightSign, leftTopPos);
	}





























}
