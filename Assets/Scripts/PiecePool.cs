using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool : MonoBehaviour
{
	public GameObject piecePrefab;
	public int piecePoolSize = 4;
	public GameObject SlingObj;

	private GameObject[] pieces;
	private Vector2 objPoolPos = new Vector2(0, -10f);
	private float spawnInterval;
	public int currentPiece = 0;

    // Start is called before the first frame update
    void Start()
    {
        pieces = new GameObject[piecePoolSize];
        for(int i=0; i<piecePoolSize; i++)
        {
        	pieces[i] = (GameObject)Instantiate(piecePrefab, objPoolPos, Quaternion.identity);
        }
        HookNewPiece();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("r"))
        // {
        //     pieces[1].transform.position = new Vector3(0,0,0);
        //     pieces[1].transform.rotation = Quaternion.Euler(0,0,0);
        // }
    }

    public void HookNewPiece()
    {
        var pieceObj = pieces[currentPiece].GetComponent<Piece>();
        pieces[currentPiece].transform.position = new Vector3(0, -2.25f, 0);
    	pieces[currentPiece].transform.SetParent(SlingObj.transform,false);
        pieceObj.isHooked = true;
        pieceObj.GetComponent<Rigidbody2D>().isKinematic = true;
        currentPiece++;
        if(currentPiece >= piecePoolSize)
            currentPiece=0;
    }
}
