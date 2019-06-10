using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSwinging : MonoBehaviour
{

	public float amplitudeMove = 0.5f;
	public float amplitudeRotate = 1f;
	public Rigidbody2D rb2d;

	private float maxSwingingAngle = 5f;
	private float minSwingingAngle = -5f;
	private float angle;
	private float angularSpeed = 1f;

	public Vector3 swingingCenter;

	int stackPlacement;
	bool rotateRight;

	void Start()
	{
		angle = 0;
		rb2d = GetComponent<Rigidbody2D>();
		swingingCenter = new Vector3(0, -4, 0);
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
			// transform.position = new Vector3(Mathf.Cos(Time.time)*amplitudeMove, transform.position.y, transform.position.z);
			// rb2d.velocity = new Vector2(Mathf.Cos(Time.time)*amplitudeMove, 0);
			// transform.rotation = Quaternion.Euler(0,0,-Mathf.Sin(Time.time)*amplitudeRotate);

			float swingingSpeed = Mathf.Cos(angle) * amplitudeRotate;
			angle += angularSpeed * Time.fixedDeltaTime;
			transform.RotateAround(swingingCenter, Vector3.forward, swingingSpeed * Time.fixedDeltaTime);
			Debug.DrawLine (new Vector3(0,-30,0), new Vector3(0,30,0),Color.red);
			Debug.DrawLine (new Vector3(transform.position.x,-30,0), new Vector3(transform.position.x,30,0),Color.yellow);			
		}
		else
		{
			rb2d.velocity = Vector2.zero;
		}
	}

	public void SwingingCenterMoveUp()
	{
			swingingCenter.y ++;
	}

	public void getAmplitudeMove()
	{
		
	}
}
