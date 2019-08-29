using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordControl : MonoBehaviour
{

	public GameObject wordPrefab;
	public GameObject word;
	public Sprite img;

	public void SpawnWord(string name)
	{
		string pfbPath = "word/prefabs/word";
		string imgPath = "word/images/" + name;
		wordPrefab = Resources.Load<GameObject>(pfbPath);
		img = Resources.Load<Sprite>(imgPath);


		Vector3 centerPos = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 8f));

		float offsetY = 3;
		Vector3 spawnPos = new Vector3(
			centerPos.x, centerPos.y + offsetY, 0f);

		word = Instantiate(wordPrefab, spawnPos, Quaternion.identity);
		word.GetComponent<SpriteRenderer>().sprite = img;
		word.transform.SetParent(this.transform);

	}
}
