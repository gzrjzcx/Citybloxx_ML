using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class StackAgent : Agent
{
	public Piece pieceObj;
    public Rigidbody2D targetRb2d;
    public Transform targetTran;
    public Rigidbody2D columnRb2d;
    public Transform columnTran;
    public Rigidbody2D agentRb2d;
    public Transform root;


    public override void InitializeAgent()
    {
    	columnTran = GameControl.instance.columnObj.transform;
    	columnRb2d = GameControl.instance.columnObj.GetComponent<Rigidbody2D>();
    	root = GameControl.instance.aiObj.root;
    }

	public override void CollectObservations()
	{

        Vector2 agentPos = root.transform.InverseTransformPoint(agentRb2d.position);
        agentPos.x = agentPos.x / 1.3f;
        agentPos.y = (agentPos.y - GameControl.instance.aiObj.agent_min_y) / 1.2f;

        Vector2 targetPos = root.transform.InverseTransformPoint(targetRb2d.position);
        targetPos.x = targetPos.x / 2.1f;
        targetPos.y = (targetPos.y - GameControl.instance.aiObj.target_min_y) / 0.2f;

        Debug.Log("agentPos = "  + agentPos + "targetPos = " + targetPos);

        AddVectorObs(agentPos); // 2
        AddVectorObs(targetPos); // 2
        AddVectorObs(agentRb2d.rotation / 20f); // 1
        AddVectorObs(targetRb2d.rotation / 15f); // 1
        AddVectorObs(columnTran.localPosition.x / 0.5f); // 1
        AddVectorObs(columnRb2d.rotation / 15f); // 1
        AddVectorObs(targetTran.localPosition.x / 0.5f); // 1
	}

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		int dropSignal = Mathf.FloorToInt(vectorAction[0]);
		if(dropSignal == 1)
		{
			// pieceObj.dropSignal = true;
			GameControl.instance.aiObj.alarmObj.SpawnAlarm();
			Time.timeScale = 0.1f;
		}
		else
		{
			RequestDecision();
		}
	}

}
