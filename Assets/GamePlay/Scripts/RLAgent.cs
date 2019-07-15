using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class RLAgent : Agent
{
	public Piece pieceObj;
	public Transform mlTarget;
	public Transform mlColumn;

	public override void CollectObservations()
	{
		AddVectorObs(this.transform.position);
        AddVectorObs(mlTarget.position);
		AddVectorObs(mlTarget.localPosition);
        AddVectorObs(mlColumn.eulerAngles.z);
        AddVectorObs(mlColumn.position.x);

        Debug.Log("this.position" + this.transform.position, gameObject);
        Debug.Log("target.position" + mlTarget.position, gameObject);
	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		int dropSignal = Mathf.FloorToInt(vectorAction[0]);
		Debug.Log("dropSignal = " + dropSignal, gameObject);
		if(dropSignal == 1)
		{
			pieceObj.dropSignal = true;
		}
	}
}
