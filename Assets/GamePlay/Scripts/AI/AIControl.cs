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

    public float agent_min_y = 2.2f;
    public float target_min_y = -0.9f;

	private bool isNeedHelp = false;

	public bool isDDA = false;
	public bool isATS = false;

	void Update()
	{
		if(stackAgentObj.pieceObj.isHooked)
		{
			if(Input.GetKeyDown(KeyCode.H))
			{
				AutoStack();
			}
			if(isNeedHelp)
			{
				stackAgentObj.RequestDecision();
				isNeedHelp = false;
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
			isNeedHelp = true;
		}
	}

	public void SetRLTarget4stack()
	{
		GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;
		stackAgentObj.targetTran = topPiece.transform;
		stackAgentObj.targetRb2d = topPiece.GetComponent<Rigidbody2D>();
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
		if(GameControl.instance.stackedPieceNum < 4)
			return;
		float columnIncrement = GameControl.instance.columnObj.columnHeightIncrement;
		float increnment = columnIncrement > 0.5 ? columnIncrement : 1;
		agent_min_y += increnment;
		target_min_y += increnment;
		Debug.Log("add min pos Y , increnment = " + increnment, gameObject);
	}

	public void SetThinkingStartTime()
	{
		thinkingStartTime = Time.time;
		// Debug.Log("SetThinkingStartTime = " + thinkingStartTime);
	}




























}
