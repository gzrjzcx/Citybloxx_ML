using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
	public RLAgent rlAgentObj;

	private bool isNeedHelp = false;

	void Update()
	{
		if(rlAgentObj.pieceObj.isHooked)
		{
			if(Input.GetKeyDown(KeyCode.H))
			{
				AIPlay();
			}
			if(isNeedHelp)
			{
				rlAgentObj.RequestDecision();
			}			
		}
	}

	public void AIPlay()
	{
		SetRLTarget();
		isNeedHelp = true;
	}

	public void SetRLTarget()
	{
		GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;
		rlAgentObj.mlTarget = topPiece.transform;
		rlAgentObj.mlColumn = GameControl.instance.columnObj.GetComponent<Transform>();
	}
}
