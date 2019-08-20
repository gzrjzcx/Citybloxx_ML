using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
	public DDAAgent ddaAgentObj;
	public StackAgent stackAgentObj;
	public Transform root;
    public Rigidbody2D columnRb2d;
    public Transform columnTran;

    public AlarmControl alarmObj;
    public AttentionControl attentionObj;

    public float thinkingStartTime = 0;

    public float agent_min_y;
    public float[] pieces_min_y;

	public bool isDDA = false;
	public bool isATS = false;
	public bool isATSTest;

	void Start()
	{
		agent_min_y = 2.2f;
		int i=0;
		foreach(PiecePosRange p in stackAgentObj.piecesDataList)
		{
			pieces_min_y[i] = p.minPosY;
			i++;
		}
	}

	void Update()
	{
		if(stackAgentObj.pieceObj.isHooked)
		{
			if(Input.GetKeyDown(KeyCode.H))
			{
				AutoStack();
				GameControl.instance.tester.helpNum++;
				// isATSTest = true;
			}		
		}
	}

	public void AdjustDifficulty()
	{
		if(isDDA)
		{
			ddaAgentObj.RequestDecision();
		}
	}

	public void AutoStack()
	{
		if(isATS)
		{
			SetRLTarget4stack();
			stackAgentObj.ResetPos();
			stackAgentObj.isPlaying = true;
		}
		// Debug.Log("----  AutoStack Start ------ | " + GameControl.instance.stackedPieceNum);
		// SetRLTarget4stack();
		// stackAgentObj.ResetPos();
		// stackAgentObj.isPlaying = true;
	}

	public void SetRLTarget4stack()
	{
		GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;
		stackAgentObj.targetTran = topPiece.transform;
		stackAgentObj.targetRb2d = topPiece.GetComponent<Rigidbody2D>();
		stackAgentObj.piecesList.Clear();
		foreach(GameObject go in GameControl.instance.columnPiecesObj.columnPieces)
		{
			stackAgentObj.piecesList.Add(go);
		}
	}

	public void SetRLTarget4DDA()
	{
		GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;

	}

	public void MoveUpPlayArea()
	{
		root.transform.position = new Vector3(0f, 
			root.transform.position.y + 
				GameControl.instance.columnObj.columnHeightIncrement, 0f);
	}

	public void AddMinPosY()
	{
		if(GameControl.instance.stackedPieceNum < 9)
			return;
		float columnIncrement = GameControl.instance.columnObj.columnHeightIncrement;
		// float increnment = columnIncrement > 0.5 ? columnIncrement : 1;
		agent_min_y += columnIncrement;
		for(int i=0; i<9; i++)
		{
			pieces_min_y[i] += columnIncrement;
		}
	}

	public void SetThinkingStartTime()
	{
		thinkingStartTime = Time.time;
		// Debug.Log("SetThinkingStartTime = " + thinkingStartTime);
	}


























}
