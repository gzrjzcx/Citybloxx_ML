using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
	// public CBXDDAAgent ddaAgentObj;
	public StackAgent stackAgentObj;
	public Transform root;
    public Rigidbody2D columnRb2d;
    public Transform columnTran;

    public AlarmControl alarmObj;

    public float agent_min_y = 2.2f;
    public float target_min_y = -0.9f;

	private bool isNeedHelp = false;

	void Update()
	{
		if(stackAgentObj.pieceObj.isHooked)
		{
			if(Input.GetKeyDown(KeyCode.H))
			{
				AIPlay();
			}
			if(isNeedHelp)
			{
				stackAgentObj.RequestDecision();
				isNeedHelp = false;
			}			
		}
	}

	public void AIPlay()
	{
		SetRLTarget4stack();
		isNeedHelp = true;
	}

	public void SetRLTarget4stack()
	{
		GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;
		stackAgentObj.targetTran = topPiece.transform;
		stackAgentObj.targetRb2d = topPiece.GetComponent<Rigidbody2D>();
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
}
