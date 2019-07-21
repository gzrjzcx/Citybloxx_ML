using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControl : MonoBehaviour
{
	private List<string> planetName;
	private GameObject planetPrefab;
	private GameObject planet;

	void Update()
	{
		if(Input.GetKey(KeyCode.P))
		{
			SpawnPlanet();
		}
	}

	void Start()
	{
		planetName = new List<string>();
		for(int i=0; i<=8; i++)
			planetName.Add("planet_" + i.ToString());
	}

	public void SpawnPlanet()
	{
		if(GameControl.instance.stackedPieceNum < 50)
			return;

		string pfbPath = "planet/prefabs/Planet";
		string imgPath = "planet/images/planet_";
		planetPrefab = Resources.Load<GameObject>(pfbPath);
		int imgIdx = Random.Range(1, 8);
		Sprite img = Resources.Load<Sprite>(imgPath + imgIdx);

		int sign = (Random.Range(0, 2) == 1) ? 1 : -1; // 1->right
		Vector3 leftTopPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0, Screen.height, 8));

		float offsetX = (-2 - Random.Range(0f, 1f)) * sign;
		float offsetY = 3f;
		float _x = (sign == 1) ? -leftTopPos.x : leftTopPos.x;
		Vector3 spawnPos = new Vector3(_x + offsetX,
									leftTopPos.y + offsetY, 0f);

		Vector3 rotEuler = new Vector3(0, 0, Random.Range(0f, 360f));
		float scale = Random.Range(0.5f, 0.8f);

		planet = Instantiate(planetPrefab, spawnPos, Quaternion.Euler(rotEuler));
		planet.transform.SetParent(this.transform, true);
		planet.GetComponent<SpriteRenderer>().sprite = img;
		planet.transform.localScale = new Vector3(scale, scale, scale);

	}

}
