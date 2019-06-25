﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GO_Extensions;

public class ColumnSwinging : MonoBehaviour
{

	public float amplitudeMove = 0.5f;
	public float amplitudeRotate;
	public float comboAmplitudeRotate = 1f;
	public float amplitudeIncrement = 0.5f;
	public float maxAmplitudeRotate = 15f;
	public Rigidbody2D rb2d;

	// public float idealDistanceColumn2Sling; //Todo: use this control distance
	public float columnHeightIncrement;

	private float angle;
	private float angularSpeed = 1f;

	void Start()
	{
		angle = 0;
		rb2d = GetComponent<Rigidbody2D>();
	}

	// public float GetDistanceColumn2Sling
	// {
	// 	get {return GameControl.instance.slingObj.transform.position.y
	// 			+ GameControl.instance.slingObj.offsetY - transform.position.y;}
	// }

	bool IsColumnShouldRotate()
	{
		if(GameControl.instance.gameStatus != GameControl.GameStatus.GAME_OVER
			&& GameControl.instance.gameStatus != GameControl.GameStatus.GAME_START)
		{
			return true;
		}
		else
			return false;
	}

	void FixedUpdate()
	{
		// if(IsColumnShouldRotate())
		// {
			// transform.position = new Vector3(Mathf.PingPong(Time.time*speed, 2), transform.position.y, transform.position.z);
			// transform.position = new Vector3(Mathf.Cos(Time.time)*amplitudeMove, transform.position.y, transform.position.z);
			rb2d.velocity = new Vector2(Mathf.Cos(Time.time)*amplitudeMove, 0);
			transform.rotation = Quaternion.Euler(0,0,-Mathf.Sin(Time.time)*amplitudeRotate);

			// float swingingSpeed = Mathf.Cos(angle) * amplitudeRotate;
			// angle += angularSpeed * Time.fixedDeltaTime;
			// transform.RotateAround(swingingCenter, Vector3.forward, swingingSpeed * Time.fixedDeltaTime);
			Debug.DrawLine (new Vector3(0,-30,0), new Vector3(0,30,0),Color.red);
			Debug.DrawLine (new Vector3(transform.position.x,-30,0), new Vector3(transform.position.x,30,0),Color.yellow);
			Debug.DrawLine(new Vector3(GameControl.instance.seaLevel.x - 10, GameControl.instance.seaLevel.y, 0),
			 new Vector3(GameControl.instance.seaLevel.x + 10, GameControl.instance.seaLevel.y, 0), Color.blue);
		// }
		// else
		// {
		// 	rb2d.velocity = Vector2.zero;
		// }
	}

	public void SwingingCenterMoveUp()
	{
		if(GameControl.instance.stackedPieceNum >= GameControl.instance.piecePoolObj.piecePoolSize - 1)
		{
			Vector3 _pos = transform.position;
			_pos.y += columnHeightIncrement;
			transform.MoveOnlyParent(_pos);
		}
	}

	public void AddAmplitudeRotate()
	{
		if(amplitudeRotate < maxAmplitudeRotate)
		{
			amplitudeRotate += amplitudeIncrement;
		}
	}

	public void SetAmplitudeIncrementAndMax()
	{
		int totalPieces = GameControl.instance.populationScore;
		if(totalPieces < 100){
			amplitudeIncrement = 0.5f;
			maxAmplitudeRotate = 7f;
			comboAmplitudeRotate = 1f;
		}
		else if(totalPieces < 200){
			amplitudeIncrement = 1f;
			maxAmplitudeRotate = 10f;
			comboAmplitudeRotate = 2f;
		}
		else if(totalPieces < 300){
			amplitudeIncrement = 1.5f;
			maxAmplitudeRotate = 13f;
			comboAmplitudeRotate = 3f;
		}
		else{
			amplitudeIncrement = 2f;
			maxAmplitudeRotate = 15f;
			comboAmplitudeRotate = 5f;
		}
	}

	public void Set2ComboSwingingAmplitude()
	{
		if(amplitudeRotate > comboAmplitudeRotate * 2)
		{
			amplitudeRotate -= comboAmplitudeRotate;
		}
		else
		{
			amplitudeRotate = comboAmplitudeRotate;			
		}
	}

	public float GetCenterPostion()
	{
		return transform.GetCenterPosition(1);
	}

	public void FlashColumnOnCombo()
	{

	}
}
