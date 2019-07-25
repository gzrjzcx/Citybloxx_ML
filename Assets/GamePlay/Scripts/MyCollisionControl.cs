using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCollisionControl : MonoBehaviour
{
    public Collider2D dropPieceCol;
    public Collider2D topPieceCol;
    public float deltaX;

    public void SetCollisionInfo(Collision2D ctl)
    {
        dropPieceCol = ctl.otherCollider;
        topPieceCol = ctl.collider;
        deltaX = GetDeltaX();
        // Debug.Log("SetCollisionInfo : deltaX = " + deltaX);
        // GetCollidersPos();
    }

    private float GetDeltaX()
    {   
        if(topPieceCol.CompareTag("Ground"))
        {
            if(GameControl.instance.columnPiecesObj.topPiece)
            {
                topPieceCol = GameControl.instance.columnPiecesObj.topPiece.GetComponent<Collider2D>();
            }
        }

        return dropPieceLocalPos.x - topPieceLocalPos.x;
    }

    // Note this local pos is not correct when play unstacked to deadcenter anim
    public Vector3 dropPieceLocalPos
    {
        get {return dropPieceCol ? dropPieceCol.transform.localPosition : Vector3.zero;}
    }

    public Vector3 topPieceLocalPos
    {
        get {return topPieceCol ? topPieceCol.transform.localPosition : Vector3.zero;}
    }

    public void GetColumnHeightIncrement()
    {
        float topPiecePosY = dropPieceLocalPos.y;
        float dropPiecePosY = topPieceLocalPos.y;
        GameControl.instance.columnObj.columnHeightIncrement = Mathf.Abs(dropPiecePosY - topPiecePosY);
    }

    public void GetCollidersPos()
    {
        Debug.Log("dropPieceCol-" + dropPieceCol.name + " pos = " + 
            dropPieceCol.transform.localPosition.ToString("F3"), gameObject);
        Debug.Log("topPieceCol-" + topPieceCol.name + " pos = " + 
            topPieceCol.transform.localPosition.ToString("F3"), gameObject);
    }
}
