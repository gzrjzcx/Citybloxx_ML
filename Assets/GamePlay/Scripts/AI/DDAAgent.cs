using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.IO;

public class DDAAgent : Agent
{
	public Piece pieceObj;

	public float thinkingTime;
	private float delta;
	private int curGameStatus;

    public bool isEnabled;
    public bool isDebug;

	public override void CollectObservations()
	{
		AddVectorObs(thinkingTime / 12f);
		AddVectorObs((GameControl.instance.columnObj.amplitudeRotate - 5f) / 16f);
		AddVectorObs(Mathf.Abs(GameControl.instance.mycolObj.deltaX) / 5f);
		AddVectorObs(GetStepCount() / (float)agentParameters.maxStep);
	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		int state = GetCurrentState();
		int turnSignal = Mathf.FloorToInt(vectorAction[0]);
		// Debug.Log("delta = " + delta + " time = " + thinkingTime + 
		// 	" rotate = " + GameControl.instance.columnObj.amplitudeRotate 
		// 	+ " cur_state = " + state + " turn_signal = " + turnSignal, gameObject);
		if(turnSignal != 0)
		{
			GameControl.instance.aiObj.attentionObj.SpawnLevelAttention(turnSignal);
			if(turnSignal == 1)
			{
				AudioControl.instance.Play("Level_Down");
				GameControl.instance.columnObj.Set2ComboSwingingAmplitude();
			}
			else if(turnSignal == 2)
			{
				AudioControl.instance.Play("Level_Up");				
			}

			GameControl.instance.tester.adjustNum++;

		}
	}

	public float deltaDegree
	{
		get 
		{
			if(delta < 0.1)
			{
				return 0;
			}
			else if(delta < 0.5)
			{
				return (float)Mathf.Pow(delta*2, 0.3f);
			}
			else
			{
				return 1;
			}
		}
	}

	public float timeDegree
	{
		get
		{
			return (float)Mathf.Pow(thinkingTime / 12f, 0.4f);
		}
	}

	private int GetCurrentState()
	{
		float exp = 0.2f * timeDegree + 0.7f * deltaDegree
					+ 0.1f * ((GameControl.instance.columnObj.amplitudeRotate-5f) / 11f);
		if(isDebug)
		{
			Debug.Log("deltaDegree = " + deltaDegree + " " + "timeDegree = " + timeDegree + " " +
						"exp = " + exp);
		}
		if(exp < 0.3)
			return 1; // easy
		else if(exp < 0.75)
			return 0; // medium
		else if(exp <= 1)
			return 2; // difficult
		else
			return 0;
	}
}
