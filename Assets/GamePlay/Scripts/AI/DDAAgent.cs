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

    public int err_count=0, total_count=0;
    public int m2d=0, m2e=0, m2m=0;
    public int e2e=0, e2m=0, e2d=0;
    public int d2e=0, d2m=0, d2d=0;
    public string s, t;
    public int isError = 0;

    public bool isEnabled;

	private List<float> perceptionBuffer = new List<float>();

	public override void CollectObservations()
	{
		delta = Mathf.Abs(GameControl.instance.mycolObj.deltaX);
		thinkingTime = thinkingTime > 15 ? 15 : thinkingTime;
		float[] diffFactors = {thinkingTime, 
								delta,
								GameControl.instance.columnObj.amplitudeRotate};
		string[] detectableObjects = {"easy", "medium", "difficult"};
		AddVectorObs(EvaluateDiffLvl(diffFactors, detectableObjects));
		AddVectorObs(GetStepCount() / (float)agentParameters.maxStep);
		Debug.Log("delta = " + delta + " time = " + thinkingTime + 
			" rotate = " + GameControl.instance.columnObj.amplitudeRotate, gameObject);
	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		int state = GetCurrentState();
		int turnSignal = Mathf.FloorToInt(vectorAction[0]);
		// Debug.Log("current state = " + state);
		// Debug.Log("tuneSignal = " + turnSignal);
		// Accuracy(state, turnSignal);
		if(turnSignal != 0)
		{
			GameControl.instance.aiObj.attentionObj.SpawnLevelAttention(turnSignal);
		}
	}

	public List<float> EvaluateDiffLvl(float[] diffFactors, string[] diffLvl)
	{
		perceptionBuffer.Clear();
		for(int factorIdx=0; factorIdx<diffFactors.Length; factorIdx++)
		{
			float[] subList = new float[diffLvl.Length + 2];
			switch(factorIdx)
			{
				case 0:
					EvaluateTimeDiffLvl(diffFactors[factorIdx], subList, diffLvl);
					break;
				case 1:
					EvaluateDeltaDiffLvl(diffFactors[factorIdx], subList, diffLvl);
					break;
				case 2:
					EvaluateRotateDiffLvl(diffFactors[factorIdx], subList, diffLvl);
					break;
			}
			perceptionBuffer.AddRange(subList);
		}
		return perceptionBuffer;
	}

	private void EvaluateRotateDiffLvl(float factor, float[] subList, string[] diffLvl)
	{
		for(int i=0; i<diffLvl.Length; i++)
		{
			if(string.Equals(GetRotateLvl(factor), diffLvl[i]))
			{
				subList[i] = 1;
				subList[diffLvl.Length + 1] = (factor-7) / 8f;
				break;
			}
		}
	}

	private void EvaluateTimeDiffLvl(float factor, float[] subList, string[] diffLvl)
	{
		for(int i=0; i<diffLvl.Length; i++)
		{
			if(string.Equals(GetTimeLvl(factor), diffLvl[i]))
			{
				subList[i] = 1;
				subList[diffLvl.Length + 1] = factor / 15f;
				break;
			}
		}
	}

	private void EvaluateDeltaDiffLvl(float factor, float[] subList, string[] diffLvl)
	{
		for(int i=0; i<diffLvl.Length; i++)
		{
			if(string.Equals(GetDeltaLvl(factor), diffLvl[i]))
			{
				subList[i] = 1;
				subList[diffLvl.Length + 1] = deltaDegree;
				break;
			}
		}
	}

	private string GetRotateLvl(float rotate)
	{
		if(rotate < 10f && rotate >= 7f)
			return "easy";
		else if(rotate < 12f)
			return "medium";
		else if(rotate <= 15f)
			return "difficult";
		else
			return null;
	}

	private string GetDeltaLvl(float delta)
	{
		if(delta <= 0.1f)
			return "easy";
		else if(delta <= 0.5f)
			return "medium";
		else if(delta < 2.8f)
			return "difficult";
		else
			return null;
	}

	public string GetTimeLvl(float elapsedTime)
	{
		if(elapsedTime < 2.5f)
			return "easy";
		else if(elapsedTime < 7.5)
			return "medium";
		else if(elapsedTime <= 15)
			return "difficult";
		else 
			return null;
	}

	public float deltaDegree
	{
		get 
		{
			if(delta < 0.1)
			{
				return delta;
			}
			else if(delta < 0.5)
			{
				return delta * 2;
			}
			else
			{
				return 1;
			}
		}
	}

	private int GetCurrentState()
	{
		float exp = 0.2f * (thinkingTime / 15f) + 0.7f * deltaDegree
					+ 0.1f * ((GameControl.instance.columnObj.amplitudeRotate-7f) / 8f);
		if(exp < 0.2)
			return 1; // easy
		else if(exp < 0.75)
			return 0; // medium
		else if(exp <= 1)
			return 2; // difficult
		else
			return 0;
	}

    public void Accuracy(int curState, int turnSignal)
    {
		using(StreamWriter sw = new StreamWriter("dda_accuracy.txt", true))
		{
	    	total_count++;

	    	if(curState == 1)
	    	{
	    		s = "easy";
	    		if(turnSignal == 2)
	    		{
	    			t = "turn_easy";
	    			err_count++;
	    			isError = 1;
	    			e2e++;
	    		}
	    		else if(turnSignal == 0)
	    		{
	    			t = "turn_medi";
	    			isError = 0;
	    			e2m++;
	    		}
	    		else if(turnSignal == 1)
	    		{
	    			t = "turn_difficult";
	    			isError = 0;
	    			e2d++;
	    		}
	    	}
	    	else if(curState == 0)
	    	{
	    		s = "medi";
	    		if(turnSignal == 1)
	    		{
	    			t = "turn_diff";
	    			isError = 0;
	    			m2d++;
	    		}
	    		else if(turnSignal == 0)
	    		{
	    			t = "turn_medi";
	    			m2m++;
	    			isError = 0;
	    		}
	    		else if(turnSignal == 2)
	    		{
	    			t = "turn_easy";
	    			isError = 0;
	    			m2e++;
	    		}
	    	}
	    	else if(curState == 2)
	    	{
	    		s = "diff";
	    		if(turnSignal == 1)
	    		{
	    			t = "turn_diff";
	    			isError = 1;
	    			err_count++;
	    			d2d++;		
	    		}
	    		else if(turnSignal == 0)
	    		{
	    			t = "turn_medi";
	    			isError = 0;
	    			d2m++; 			
	    		}
	    		else if(turnSignal == 2)
	    		{
	    			t = "turn_easy";
	    			isError = 0;
	    			d2e++;
	    		}
	    	}
		
			sw.WriteLine(
			 isError + "\t" + delta.ToString("F2") + "\t" + thinkingTime + "\t" +  
			 GameControl.instance.columnObj.amplitudeRotate + "\t" +
			 s + "\t" + t + "\t" + err_count + "\t" +
			 e2e + "\t" + e2m + "\t" + e2d + "\t" +
			 m2e + "\t" + m2m + "\t" + m2d + "\t" +
			 d2e + "\t" + d2m + "\t" + d2d);
		}

    }

}
