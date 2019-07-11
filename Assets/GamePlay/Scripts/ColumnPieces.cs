using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPieces : MonoBehaviour
{

	public List<GameObject> columnPieces;

    // Start is called before the first frame update
    void Start()
    {
		List<GameObject> columnPieces = new List<GameObject>();        
    }

    public void Add(GameObject go)
    {
    	columnPieces.Add(go);
    }

    public void AddWithMaxNum(GameObject go, int max)
    {
    	Add(go);
    	if(columnPieces.Count > max)
    		Remove(lowestPiece);
    }

    public void Remove(GameObject go)
    {
    	columnPieces.Remove(go);
    }

    public GameObject topPiece 
    {
    	get { return columnPieces[columnPieces.Count - 1];}
	}

	public GameObject lowestPiece
	{
		get { return columnPieces[0];}
	}
}
