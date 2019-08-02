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

    private List<float> perceptionBuffer = new List<float>();
    public List<GameObject> piecesList = new List<GameObject>();
    public List<PiecePosRange> piecesDataList = new List<PiecePosRange>();

    public override void InitializeAgent()
    {
    	columnTran = GameControl.instance.columnObj.transform;
    	columnRb2d = GameControl.instance.columnObj.GetComponent<Rigidbody2D>();
    	root = GameControl.instance.aiObj.root;
    }

	public override void CollectObservations()
	{
        // float rot = (GameControl.instance.columnObj.amplitudeRotate - 5f) / 10f;
        Vector2 agentPos = root.transform.InverseTransformPoint(agentRb2d.position);
        agentPos.x = (agentPos.x + 1.42f) / 2.72f;
        agentPos.y = (agentPos.y - 2.2f) / 1.2f;

        AddVectorObs(agentPos); // 2
        AddVectorObs((agentRb2d.rotation + 20f) / 40f); // 1

        AddVectorObs((columnTran.localPosition.x + 0.5f) / 1f); // 1
        AddVectorObs((columnRb2d.rotation + 15f) / 30f); // 1

        AddVectorObs(PerceptPieces()); // 36

        // AddVectorObs(rot); // 1
	}

    public List<float> PerceptPieces()
    {
        perceptionBuffer.Clear();
        for(int i=0; i<piecesList.Count; i++)
        {
            float[] sublist = new float[4];
            SetSubList(piecesList[i], sublist, i);
            perceptionBuffer.AddRange(sublist);
        }
        return perceptionBuffer;
    }

    private void SetSubList(GameObject piece, float[] subList, int idx)
    {
        Rigidbody2D pieceRb2d = piece.GetComponent<Rigidbody2D>();
        Vector2 piecePos = root.transform.InverseTransformPoint(pieceRb2d.position);
        piecePos.x = (piecePos.x - piecesDataList[idx].minPosX) / piecesDataList[idx].posRangeX;
        piecePos.y = (piecePos.y - piecesDataList[idx].minPosY) / piecesDataList[idx].posRangeY;
        subList[0] = piecePos.x;
        subList[1] = piecePos.y;
        subList[2] = (pieceRb2d.rotation + 15f) / 30f;
        subList[3] = (piece.transform.localPosition.x + (0.5f * (idx+1))) / (1 * (idx+1));
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
