using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool : MonoBehaviour
{
	public GameObject piecePrefab;
	public int piecePoolSize = 4;
	public GameObject SlingObj;
    public int currentPieceIdx = 0;
    public string topPieceIdx;

	private GameObject[] pieces;
	private Vector2 objPoolPos = new Vector2(0, -10f);
	private float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        pieces = new GameObject[piecePoolSize];
        for(int i=0; i<piecePoolSize; i++)
        {
        	pieces[i] = (GameObject)Instantiate(piecePrefab, objPoolPos, Quaternion.identity);
            pieces[i].gameObject.name = "Piece" + i.ToString();
            pieces[i].gameObject.tag = "Piece";
        }
        HookNewPiece();
    }

    public void HookNewPiece()
    {
        var pieceObj = pieces[currentPieceIdx].GetComponent<Piece>();
        pieceObj.transform.parent = null; // avoid x offset when hooking the piece from column
        pieceObj.transform.position = new Vector3(0, -2.25f, 0);
    	pieceObj.transform.SetParent(SlingObj.transform,false);
        pieceObj.isHooked = true;
        pieceObj.isStacked = false;
        pieceObj.GetComponent<Rigidbody2D>().isKinematic = true;
        
        currentPieceIdx++;
        if(currentPieceIdx >= piecePoolSize)
        {
            currentPieceIdx=0;
        }
    }
}
