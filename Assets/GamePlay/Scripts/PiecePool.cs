using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool : MonoBehaviour
{
	public GameObject piecePrefab;
	public int piecePoolSize = 4;
	public GameObject SlingObj;
    public int currentPieceIdx = 0;
    public int lastPieceIdx = 0;
	public GameObject[] pieces;
    public Transform idlePieceArea;
	
    private Vector3 objPoolPos;
	private float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        objPoolPos = GameControl.instance.seaLevel;
        pieces = new GameObject[piecePoolSize];
        for(int i=0; i<piecePoolSize; i++)
        {
        	pieces[i] = (GameObject)Instantiate(piecePrefab, objPoolPos, Quaternion.identity);
            pieces[i].transform.SetParent(idlePieceArea);
            pieces[i].gameObject.name = "Piece" + i.ToString();
            pieces[i].gameObject.tag = "Piece";
        }
        HookNewPiece();
    }

    public void HookNewPiece()
    {
        var pieceObj = pieces[currentPieceIdx].GetComponent<Piece>();
        // pieceObj.transform.parent = null; // avoid x offset when hooking the piece from column
    	pieceObj.transform.SetParent(SlingObj.transform,false);
        pieceObj.transform.localPosition = new Vector3(0, -2.25f, 0);
        pieceObj.isHooked = true;
        pieceObj.isStacked = false;
        pieceObj.GetComponent<Rigidbody2D>().isKinematic = true;

        GameControl.instance.columnPiecesObj.Remove(pieces[currentPieceIdx]);
        GameControl.instance.aiObj.stackAgentObj = pieces[currentPieceIdx].GetComponent<StackAgent>();
        GameControl.instance.aiObj.ddaAgentObj = pieces[currentPieceIdx].GetComponent<DDAAgent>();
        GameControl.instance.aiObj.SetThinkingStartTime();

        lastPieceIdx = currentPieceIdx;
        currentPieceIdx++;
        if(currentPieceIdx >= piecePoolSize)
        {
            currentPieceIdx=0;
            if(GameControl.instance.aiObj.isATSTest)
                GameControl.instance.aiObj.stackAgentObj.ResetPos();
        }
        if(GameControl.instance.aiObj.isATSTest)
            Invoke("AutoStack", 1f);
    }

    public void HookFailedPiece()
    {
        var pieceObj = pieces[lastPieceIdx].GetComponent<Piece>();
        // pieceObj.transform.parent = null; // avoid x offset when hooking the piece from column
        pieceObj.transform.SetParent(SlingObj.transform,false);
        pieceObj.transform.localPosition = new Vector3(0, -2.25f, 0);
        pieceObj.isHooked = true;
        pieceObj.isStacked = false;
        pieceObj.GetComponent<Rigidbody2D>().isKinematic = true;

        GameControl.instance.aiObj.stackAgentObj = pieces[lastPieceIdx].GetComponent<StackAgent>();
        GameControl.instance.aiObj.ddaAgentObj = pieces[lastPieceIdx].GetComponent<DDAAgent>();
        GameControl.instance.aiObj.SetThinkingStartTime();
        if(GameControl.instance.aiObj.isATSTest)
            Invoke("AutoStack", 1f);
    }

    void AutoStack()
    {
        GameControl.instance.aiObj.AutoStack();
    }
























}
