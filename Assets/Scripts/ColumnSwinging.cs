using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSwinging : MonoBehaviour
{

	public int totalStacked;
	public float swingSpeed;
	public float maxAngle;
	public int swinger;

	public float amplitudeMove = 0.5f;
	public float amplitudeRotate = 1f;

	public Rigidbody2D rb2d;

	private float maxSwingingAngle = 5f;
	private float minSwingingAngle = -5f;
	private float angle;
	private Vector3 pos;

	int stackPlacement;
	bool rotateRight;

	void Start()
	{
		angle = 0;
		rb2d = GetComponent<Rigidbody2D>();
	}

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
		if(IsColumnShouldRotate())
		{
			// transform.position = new Vector3(Mathf.PingPong(Time.time*speed, 2), transform.position.y, transform.position.z);
			rb2d.velocity = new Vector2(Mathf.Cos(Time.time)*amplitudeMove, 0);
			// transform.position = new Vector3(Mathf.Cos(Time.time)*amplitudeMove, transform.position.y, transform.position.z);
			transform.rotation = Quaternion.Euler(0,0,-Mathf.Sin(Time.time)*amplitudeRotate);
			Debug.DrawLine (new Vector3(0,-30,0), new Vector3(0,30,0),Color.red);
			Debug.DrawLine (new Vector3(transform.position.x,-30,0), new Vector3(transform.position.x,30,0),Color.yellow);			
		}
		else
		{
			rb2d.velocity = Vector2.zero;
		}
	}

	public void getAmplitudeMove()
	{
		
	}
}
