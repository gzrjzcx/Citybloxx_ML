using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFOControl : MonoBehaviour
{
	private List<string> ufoName;
	private GameObject ufoPrefab;
	private GameObject ufo;

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
		string pfbPath = "ufo/prefabs/ufo";
		string imgPath = "ufo/images/ufo_";
		ufoPrefab = Resources.Load<GameObject>(pfbPath);
		// int imgIdx = Random.Range(1, 4);
		int imgIdx = 5;
		Sprite img = Resources.Load<Sprite>(imgPath + imgIdx);

		int sign = (Random.Range(0, 2) == 1) ? 1 : -1; // 1->right
		SpawnRocket(sign, img);
	}

	private void SpawnSaucer()
	{

		string pfbPath = "ufo/prefabs/ufo";
		string imgPath = "ufo/images/ufo_";
		ufoPrefab = Resources.Load<GameObject>(pfbPath);
		int imgIdx = Random.Range(1, 4);
		Sprite img = Resources.Load<Sprite>(imgPath + imgIdx);

		int sign = (Random.Range(0, 2) == 1) ? 1 : -1; // 1->right
		Vector3 leftTopPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, Screen.height, 8));

		float offsetX = 3f * sign;
		float offsetY = 3f;
		float _x = (sign == 1) ? -leftTopPos.x : leftTopPos.x;
		Vector3 spawnPos = new Vector3(_x + offsetX,
									leftTopPos.y + offsetY, 0f);

		Vector3 rotEuler = new Vector3(0, 0, Random.Range(0f, 360f));
		float scale = Random.Range(0.5f, 0.8f);

		ufo = Instantiate(ufoPrefab, spawnPos, Quaternion.Euler(rotEuler));
		ufo.transform.SetParent(this.transform, true);
		ufo.GetComponent<SpriteRenderer>().sprite = img;
		ufo.transform.localScale = new Vector3(scale, scale, scale);

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

		Vector3 rotEuler = new Vector3(0f, (sign == 1) ? 180f : 0, -60f);
		float scale = 0.5f;
		ufo = Instantiate(ufoPrefab, spawnPos, Quaternion.Euler(rotEuler));
		ufo.transform.localScale = new Vector3(scale, scale, 0);
		ufo.transform.SetParent(this.transform);
		SpaceFO ufoObj = ufo.GetComponent<SpaceFO>();
		ufoObj.spaceFOType = SpaceFO.SpaceFOType.ROCKET;

	}

	private void SpawnSpaceShip()
	{

	}

	private void SpawnSatellite()
	{
		
	}

}
