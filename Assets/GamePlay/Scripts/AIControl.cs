using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
	// public CBXDDAAgent rlAgentObj;

	// private bool isNeedHelp = false;

	// void Update()
	// {
	// 	if(rlAgentObj.pieceObj.isHooked)
	// 	{
	// 		if(Input.GetKeyDown(KeyCode.H))
	// 		{
	// 			AIPlay();
	// 		}
	// 		if(isNeedHelp)
	// 		{
	// 			rlAgentObj.RequestDecision();
	// 			isNeedHelp = false;
	// 		}			
	// 	}
	// }

	// public void AIPlay()
	// {
	// 	SetRLTarget4DDA();
	// 	isNeedHelp = true;
	// }

	// public void SetRLTarget4stack()
	// {
	// 	GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;
	// 	rlAgentObj.mlTarget = topPiece.transform;
	// 	rlAgentObj.mlColumn = GameControl.instance.columnObj.GetComponent<Transform>();
	// }

	// public void SetRLTarget4DDA()
	// {
	// 	GameObject topPiece = GameControl.instance.columnPiecesObj.topPiece;
	// 	rlAgentObj.mlTarget = topPiece.transform;
	// 	rlAgentObj.mlColumn = GameControl.instance.columnObj.GetComponent<Transform>();
	// }
}
