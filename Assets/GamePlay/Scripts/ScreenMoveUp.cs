using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenMoveUp : MonoBehaviour
{

	public GameObject skyObj_1;
	public GameObject skyObj_2;
	public GameObject cameraObj;
	public GameObject obstacleObj;
	public Transform shadowObj_1;
	public Transform shadowObj_2;

	public float pieceSize = 1f;
	public float backgroundHeight = 10f;
	public float moveSpeed = 2f;

	private float slingMoveStep;
	private Vector3 cameraDestination;
	private float slingPosYOffsetDestination;
	private Vector3 shadowPosYOffsetDestination_1;
	private Vector3 shadowPosYOffsetDestination_2;
	private bool isFinishedOneDrop = false;
	[SerializeField]
	private List<string> bgName;
	private int curBGIdx = 2;

	void Start()
	{
		bgName = new List<string>();
		for(int i=1; i<=15; i++)
		{
			bgName.Add("bg" + i.ToString());
		}
	}

	public Vector3 cameraPos
	{
		get {return cameraObj.transform.position;}
	}

	void FixedUpdate()
	{
		if(GameControl.instance.isGameRunning)
		{
			cameraObj.transform.position = Vector3.MoveTowards(
				cameraObj.transform.position, cameraDestination, moveSpeed*Time.fixedDeltaTime);
			GameControl.instance.slingObj.offsetY = Mathf.MoveTowards(
				GameControl.instance.slingObj.offsetY, slingPosYOffsetDestination, moveSpeed*Time.fixedDeltaTime);
		}
	}

	public void MoveUp()
	{
		BGMoveUp_v2();
		ObstacleMoveUp();
		SetCameraMoveDestination();
		SetSlingMoveDestination();
	}

	private void SetCameraMoveDestination()
	{
		cameraDestination = cameraObj.transform.position;
		cameraDestination.y += GameControl.instance.columnObj.columnHeightIncrement;
		// cameraObj.transform.DOMoveY(GameControl.instance.columnObj.columnHeightIncrement, 0.5);
	}

	private void SetSlingMoveDestination()
	{
		slingPosYOffsetDestination = GameControl.instance.slingObj.offsetY;
		slingPosYOffsetDestination += GameControl.instance.columnObj.columnHeightIncrement;
		// GameControl.instance.slingObj.offsetY += GameControl.instance.columnObj.columnHeightIncrement;
		// GameControl.instance.slingObj.transform.DOMoveY(GameControl.instance.columnObj.columnHeightIncrement, 0.5);
	}

	private void SetShadowMoveDestinantion()
	{
		shadowPosYOffsetDestination_1 = shadowObj_1.position;
		shadowPosYOffsetDestination_1.y -= GameControl.instance.columnObj.columnHeightIncrement;
		shadowPosYOffsetDestination_2 = shadowObj_2.position;
		shadowPosYOffsetDestination_2.y -= GameControl.instance.columnObj.columnHeightIncrement;
	}

	private void BGMoveUp_v2()
	{
		Vector3 posSkyObj_1 = skyObj_1.transform.position;
		Vector3 posSkyObj_2 = skyObj_2.transform.position;
		Vector3 posCamera = cameraObj.transform.position;
		string bgPath = "bg/bg";
		float bgHeight = 20.4f;
		float half_h = 10.2f;

		// Debug.Log("sky1.posY = " + posSkyObj_1.y);
		// Debug.Log("sky2.posY = " + posSkyObj_2.y);
		// Debug.Log("seaLevel. y = " + GameControl.instance.seaLevel.y);
		Sprite bg = Resources.Load<Sprite>(bgPath + curBGIdx.ToString());

		if(posSkyObj_1.y < posSkyObj_2.y)
		{
			if(GameControl.instance.seaLevel.y >= posSkyObj_1.y + half_h)
			{
				curBGIdx ++;
				if(curBGIdx > 14)
					curBGIdx = 4;
				skyObj_1.GetComponent<SpriteRenderer>().sprite = bg;
				posSkyObj_1.y += 2 * bgHeight;
				skyObj_1.transform.position = posSkyObj_1;
			}
		}
		else
		{
			if(GameControl.instance.seaLevel.y >= posSkyObj_2.y + half_h)
			{
				curBGIdx ++;
				if(curBGIdx > 14)
					curBGIdx = 4;
				skyObj_2.GetComponent<SpriteRenderer>().sprite = bg;
				posSkyObj_2.y += 2 * bgHeight;
				skyObj_2.transform.position = posSkyObj_2;
			}
		}
	}

	public void ShakeCamera()
	{
		cameraObj.transform.DOShakePosition(0.5f, new Vector3(0.1f, 0.1f, 0), 50, 60);
	}

	public void ObstacleMoveUp()
	{
		obstacleObj.transform.position = GameControl.instance.seaLevel;
	}










}
