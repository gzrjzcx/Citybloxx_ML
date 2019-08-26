using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEditor;

public class StackAgent : Agent
{
	public Piece pieceObj;
    public Rigidbody2D targetRb2d;
    public Transform targetTran;
    public Rigidbody2D agentRb2d;
    public Transform agentTran;
    public Transform root;

    public Vector2 agentVelocity;
    public Vector2[] piecesVelocity;
    public Vector2 agentLastPos;
    public Vector2[] piecesLastPos;

    private List<float> perceptionBuffer = new List<float>();
    public List<GameObject> piecesList = new List<GameObject>();
    public List<PiecePosRange> piecesDataList = new List<PiecePosRange>();

    public bool isPlaying;

    public override void InitializeAgent()
    {
    	root = GameControl.instance.aiObj.root;
    }

    Vector2 pos2root(Vector2 pos)
    {
        return root.transform.InverseTransformPoint(pos);
    }

    public void ResetPos()
    {
        Vector2 agentWorldPos = new Vector2(agentTran.position.x, agentTran.position.y);
        agentLastPos = pos2root(agentWorldPos);
        for(int i=0; i<piecesList.Count; i++)
        {
            Vector2 pieceWorldPos = new Vector2(piecesList[i].transform.position.x, piecesList[i].transform.position.y);
            piecesLastPos[i] = pos2root(pieceWorldPos);
            piecesVelocity[i] = Vector2.zero;
        }
    }

    void SetSpeed()
    {
        Vector2 agentWorldPos = new Vector2(agentTran.position.x, agentTran.position.y);
        agentVelocity = (pos2root(agentWorldPos) - agentLastPos) / Time.deltaTime;
        agentLastPos = pos2root(agentWorldPos);
        // Debug.Log("SetSpeed : agentPos = " + agentWorldPos + "  normalize: " + (agentWorldPos.y - 2.2f) / 1.2f, gameObject);
        for(int i=0; i<piecesList.Count; i++)
        {
            Vector2 pieceWorldPos = new Vector2(piecesList[i].transform.position.x, piecesList[i].transform.position.y);
            piecesVelocity[i] = (pos2root(pieceWorldPos) - piecesLastPos[i]) / Time.deltaTime;
            piecesLastPos[i] = pos2root(pieceWorldPos);
        }
    }

    void FixedUpdate()
    {
        if(isPlaying)
        {
            SetSpeed();
            RequestDecision();
        }            
    }

	public override void CollectObservations()
	{
        float rot = (GameControl.instance.columnObj.amplitudeRotate - 5f) / 10f;
        Vector2 agentPos = pos2root(new Vector2(agentTran.position.x, agentTran.position.y));
        // Debug.Log("agentPos = " + agentPos + "  normalize: " + (agentPos.y - agentTran.position.y) / 1.2f, gameObject);
        agentPos.x = (agentPos.x + 1.42f) / 2.72f;
        agentPos.y = (agentPos.y - GameControl.instance.aiObj.agent_min_y) / 1.3f;
        AddVectorObs(agentPos); // 2

        float agentRot = agentTran.eulerAngles.z > 180 ? 
            (agentTran.eulerAngles.z-360) : agentTran.eulerAngles.z;
        AddVectorObs((agentRot + 20f) / 40f); // 1

        AddVectorObs(agentVelocity); // 2
        AddVectorObs(PerceptPieces()); // 54
        AddVectorObs(rot); // 1
	}

    public List<float> PerceptPieces()
    {
        perceptionBuffer.Clear();
        for(int i=0; i<piecesList.Count; i++)
        {
            float[] sublist = new float[6];
            SetSubList(piecesList[i], sublist, i);
            perceptionBuffer.AddRange(sublist);
        }
        return perceptionBuffer;
    }

    private void SetSubList(GameObject piece, float[] subList, int idx)
    {
        Vector2 piecePos;
        Vector2 pieceWorldPos = pos2root(new Vector2(piece.transform.position.x, piece.transform.position.y));
        // Debug.Log(piece.name + " y = " + piecePos.y + " normalize: " +
        //      (piecePos.y - GameControl.instance.aiObj.pieces_min_y[idx]) / piecesDataList[idx].posRangeY);
        piecePos.x = (pieceWorldPos.x - piecesDataList[idx].minPosX) / piecesDataList[idx].posRangeX;
        piecePos.y = (pieceWorldPos.y - GameControl.instance.aiObj.pieces_min_y[idx]) / piecesDataList[idx].posRangeY;
        float rot = piece.transform.eulerAngles.z > 180 ? (piece.transform.eulerAngles.z - 360) : piece.transform.eulerAngles.z;        
        subList[0] = piecePos.x;
        subList[1] = piecePos.y;
        subList[2] = (rot + 15f) / 30f;
        subList[3] = (piece.transform.localPosition.x + (0.5f * (idx+1))) / (1 * (idx+1));
        subList[4] = (piecesVelocity[idx].x - piecesDataList[idx].minVeloX) / piecesDataList[idx].veloRangeX;
        subList[5] = (piecesVelocity[idx].y - piecesDataList[idx].minVeloY) / piecesDataList[idx].veloRangeY;
        // if(subList[4] > 1f || subList[5] > 1f)
        // {
        //     // Debug.Log("Raw velocity: = " + piecesVelocity[idx] + " min = " + piecesDataList[idx].minVeloX + " " +piecesDataList[idx].minVeloY);
        //     // Debug.Log("vel_x = " + subList[4] + " vel_y = " + subList[5] + " " + piece.name);
        //     // EditorApplication.isPaused = true;
        // }

        // if(subList[0] > 1f || subList[1] > 1f)
        // {
        //     // Debug.Log("Raw pos: = " + pieceWorldPos + " Normalize: " + piecePos + 
        //     //     " min_x = " + piecesDataList[idx].minPosX + " min_y = " + GameControl.instance.aiObj.pieces_min_y[idx] + " " + piece.name);
        //     // EditorApplication.isPaused = true;
        // }

    }

	public override void AgentAction(float[] vectorAction, string textAction)
	{
		int dropSignal = Mathf.FloorToInt(vectorAction[0]);
		if(dropSignal == 1)
		{
            // GameControl.instance.tester.isATSStack = true;
			// pieceObj.dropSignal = true;
			GameControl.instance.aiObj.alarmObj.SpawnAlarm();
			Time.timeScale = 0.01f;
            isPlaying = false;
		}
	}

}
