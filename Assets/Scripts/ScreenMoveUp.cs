using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMoveUp : MonoBehaviour
{

	public GameObject skyObj_1;
	public GameObject skyObj_2;
	public GameObject cameraObj;

	public void MoveUp()
	{
		SlingAndCameraMoveUp();
		BackgroundMoveUp();
	}

	private void SlingAndCameraMoveUp()
	{
		GameControl.instance.slingObj.offsetY++;
		Vector3 posCamera = cameraObj.transform.position;
		posCamera.y++;

		cameraObj.transform.position = posCamera;
	}

	private void BackgroundMoveUp()
	{
		Vector3 posSkyObj_1 = skyObj_1.transform.position;
		Vector3 posSkyObj_2 = skyObj_2.transform.position;
		Vector3 posCamera = cameraObj.transform.position;
		float backgroundHeight = 10f;

		if(posSkyObj_1.y < posSkyObj_2.y)
		{
			if(posCamera.y - posSkyObj_2.y >= 0)
			{
				posSkyObj_1.y += backgroundHeight;
				skyObj_1.transform.position = posSkyObj_1;
			}
		}
		else
		{
			if(posCamera.y - posSkyObj_1.y >= 0)
			{
				posSkyObj_2.y += backgroundHeight;
				skyObj_2.transform.position = posSkyObj_2;
			}
		}
	}
}
