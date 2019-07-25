using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionControl : MonoBehaviour
{
	private GameObject attentionPrefab;
	private GameObject attention;

	void Update()
	{
		if(Input.GetKey(KeyCode.A))
		{
			SpawnLevelAttention(Random.Range(0, 2));
		}
	}

    public void SpawnLevelAttention(int turnSignal)
    {
    	string name = turnSignal == 1 ? "up" : "down";

		string pfbPath = "attention/prefabs/attention";
		string imgPath = "attention/images/level_" + name;
		attentionPrefab = Resources.Load<GameObject>(pfbPath);
		Sprite img = Resources.Load<Sprite>(imgPath);

		Vector3 leftCenterPos = Camera.main.ScreenToWorldPoint(
			new Vector3(0f, Screen.height * 0.5f, 8f));

		float offsetX = 3f;
		float offsetY = 1.5f;
		Vector3 spawnPos = new Vector3(
			leftCenterPos.x - offsetX, leftCenterPos.y + offsetY, 0f);

		attention = Instantiate(attentionPrefab, spawnPos, Quaternion.identity);
		attention.transform.SetParent(this.transform);
		attention.GetComponent<SpriteRenderer>().sprite = img;
    }
}
