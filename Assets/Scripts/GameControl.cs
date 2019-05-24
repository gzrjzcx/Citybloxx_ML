using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;
	public PiecePool piecePoolObj;

	// public bool 

    // Start is called before the first frame update
    void Awake()
    {
        if(!instance)
        {
        	instance = this;
        }else if(instance)
        {
        	Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PieceStacked()
    {
    	piecePoolObj.HookNewPiece();
    }
}
