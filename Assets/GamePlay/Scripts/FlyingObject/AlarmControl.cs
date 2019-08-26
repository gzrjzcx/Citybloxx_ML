using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmControl : MonoBehaviour
{

	private GameObject alarmPrefab;
	private GameObject alarm;

	// void Update()
	// {
	// 	if(Input.GetKey(KeyCode.A))
	// 	{
	// 		SpawnAlarm();
	// 	}
	// }

	public void SpawnAlarm()
	{
		string pfbPath = "alarm/prefabs/alarm";
		alarmPrefab = Resources.Load<GameObject>(pfbPath);

		Vector3 centerPos = Camera.main.ScreenToWorldPoint(
			new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 8f));

		float offsetX = 3;
		float offsetY = 1;
		Vector3 spawnPos = new Vector3(
			centerPos.x + offsetX, centerPos.y - offsetY, 0f);

		alarm = Instantiate(alarmPrefab, spawnPos, Quaternion.identity);
		alarm.transform.SetParent(this.transform);

	}
}
