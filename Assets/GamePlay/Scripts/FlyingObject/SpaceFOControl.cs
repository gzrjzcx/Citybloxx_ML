using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFOControl : MonoBehaviour
{
	private List<string> ufoName;
	private GameObject ufoPrefab;
	private GameObject ufo;

	[SerializeField]
	private int spawnThreshold;
	[SerializeField]
	private int spawnProbability;

	void Update()
	{
		if(Input.GetKey(KeyCode.U))
		{
			SpawnFO();
		}
	}

	void Start()
	{
		ufoName = new List<string>();
		for(int i=0; i<=6; i++)
			ufoName.Add("ufo_" + i.ToString());
	}

	public void SpawnFO()
	{
		if(GameControl.instance.stackedPieceNum < spawnThreshold)
			return;

		if(Random.Range(0, spawnProbability+1) != 0)
			return;

		if(this.transform.childCount > 1)
			return;

		string pfbPath = "ufo/prefabs/ufo";
		string imgPath = "ufo/images/ufo_";
		ufoPrefab = Resources.Load<GameObject>(pfbPath);
		int imgIdx = Random.Range(1, 7);
		Sprite img = Resources.Load<Sprite>(imgPath + imgIdx);

		int sign = (Random.Range(0, 2) == 1) ? 1 : -1; // 1->right
		switch(imgIdx)
		{
			case 1:
			case 2:
				SpawnSpaceShip(sign, img);
				break;
			case 3:
				SpawnSatellite(img);
				break;
			case 4:
			case 5:
				SpawnRocket(sign, img);
				break;
			case 6:
				SpawnAlien(img);
				break;
		}
	}

	private void SpawnAlien(Sprite img)
	{
		Vector3 leftCenterPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, Screen.height * 0.5f, 8));

		float offsetX = 0f;
		float offsetY = Random.Range(0f, 3f);
		Vector3 spawnPos = new Vector3(leftCenterPos.x - offsetX,
									leftCenterPos.y - offsetY, 0f);
		Debug.Log(spawnPos);

		float scale = 0.4f;

		Vector3 rot = new Vector3(0f, 0f, 91.5f);
		ufo = Instantiate(ufoPrefab, spawnPos, Quaternion.Euler(rot));
		ufo.transform.localScale = new Vector3(scale, scale, 0);
		ufo.transform.SetParent(this.transform);
		SpaceFO ufoObj = ufo.GetComponent<SpaceFO>();
		ufoObj.spaceFOType = SpaceFO.SpaceFOType.Alien;
	}

	private void SpawnRocket(int sign, Sprite img)
	{
		Vector3 leftCenterPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, Screen.height * 0.5f, 8));
		float offsetY = Random.Range(0f, 2f);
		float offsetX = 2f * sign;
		float _x = (sign == 1) ? -leftCenterPos.x : leftCenterPos.x;
		Vector3 spawnPos = new Vector3(
			_x + offsetX,
			leftCenterPos.y + offsetY, 0f);

		Vector3 rotEuler = new Vector3(0f, (sign == 1) ? 180f : 0f, 0f);
		float scale = 0.3f;
		ufo = Instantiate(ufoPrefab, spawnPos, Quaternion.Euler(rotEuler));
		ufo.GetComponent<SpriteRenderer>().sprite = img;
		ufo.transform.localScale = new Vector3(scale, scale, 0);
		ufo.transform.SetParent(this.transform);
		SpaceFO ufoObj = ufo.GetComponent<SpaceFO>();
		ufoObj.spaceFOType = SpaceFO.SpaceFOType.ROCKET;
		ufoObj.sign = sign;

	}

	private void SpawnSpaceShip(int sign, Sprite img)
	{
		Vector3 bottomCenterPos = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width * 0.5f, 0f, 8f));
		float offsetX = Random.Range(2f, 4f) * sign;
		float offsetY = 1f;
		Vector3 spawnPos = new Vector3(
			bottomCenterPos.x + offsetX,
			bottomCenterPos.y - offsetY, 0f);
		float scale = 0.2f;
		ufo = Instantiate(ufoPrefab, spawnPos, Quaternion.identity);
		ufo.GetComponent<SpriteRenderer>().sprite = img;
		ufo.transform.localScale = new Vector3(scale, scale, 0);
		ufo.transform.SetParent(this.transform);
		SpaceFO ufoObj = ufo.GetComponent<SpaceFO>();
		ufoObj.spaceFOType = SpaceFO.SpaceFOType.SHIP;
		ufoObj.sign = sign;
	}

	private void SpawnSatellite(Sprite img)
	{	
		Vector3 spawnPos = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width * Random.value,
				Screen.height * 1.5f, 8f));
		float scale = 0.2f;

		ufo = Instantiate(ufoPrefab, spawnPos,Quaternion.identity);
		ufo.GetComponent<SpriteRenderer>().sprite = img;
		ufo.transform.localScale = new Vector3(scale, scale, 0);
		ufo.transform.SetParent(this.transform);
		SpaceFO ufoObj = ufo.GetComponent<SpaceFO>();
		ufoObj.spaceFOType = SpaceFO.SpaceFOType.SATELLITE;
	}

















}
