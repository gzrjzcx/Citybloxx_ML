using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public DoTweenControl doTween;
    public struct StackStatus
    {
        public bool isStackSuccessful;
        public int fallenSide;
        public bool isDeadCenter;
    }
    public StackStatus stackStatus;

    public bool isHooked = false;
    public bool isStacked = true;
    public bool isFallen = false;
    public bool dropSignal = false;

    public float deadCenterRange = 0.08f;
    public float stackRange = 0.5f;

	public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(isHooked)
    	{
	        if(Input.GetKeyDown("space") || Input.touchCount > 0 || dropSignal)
	        {
                transform.SetParent(GameControl.instance.piecePoolObj.idlePieceArea, true);
                Vector3 p = transform.position;
                p.z = 0;
                transform.position = p;
	        	transform.rotation = Quaternion.identity;
	        	rb2d.isKinematic = false;
	        	isHooked = false;
                GetThinkingTime();
	        }
    	}
    }

    public void GetThinkingTime()
    {
        GameControl.instance.aiObj.ddaAgentObj.thinkingTime = 
            Time.time - GameControl.instance.aiObj.thinkingStartTime;
        // Debug.Log("GetThinkingTime = " + GameControl.instance.aiObj.ddaAgentObj.thinkingTime);
    }

    void OnCollisionEnter2D(Collision2D ctl)
    {
        if(!isStacked)
        {
            rb2d.isKinematic = true;
            rb2d.velocity = Vector3.zero;
            Parent2Column();
            if(GameControl.instance.gameStatus != GameControl.GameStatus.GAME_START)
                GameControl.instance.mycolObj.SetCollisionInfo(ctl);
        }
    }

    public void Parent2Column()
    {
        transform.SetParent(GameControl.instance.columnObj.transform, true);
        // set the rotation for subObject using this way
        transform.localEulerAngles = Vector3.zero;
        rb2d.angularVelocity = 0;
    }

    void OnCollisionExit2D(Collision2D ctl)
    {
        if(!isStacked)
        {
            switch(GameControl.instance.gameStatus)
            {
                case GameControl.GameStatus.GAME_START:
                    GameControl.instance.AfterCollisionAtGameStart();
                    GameControl.instance.columnPiecesObj.Add(this.gameObject);
                    break;
                case GameControl.GameStatus.GAME_FIRSTPIECE:
                case GameControl.GameStatus.GAME_RUNNING:
                case GameControl.GameStatus.GAME_COMBO:
                    AfterCollisionAtGameRunning();
                    break;
            }
            GameControl.instance.aiObj.AdjustDifficulty();
            GameControl.instance.piecePoolObj.HookNewPiece();
            AudioControl.instance.Play("Block_Hit");
        }

        isStacked = true;
    }

    void AfterCollisionAtGameRunning()
    {
        if(GameControl.instance.mycolObj.topPieceCol.gameObject.tag == "Piece")
        {
            GameControl.instance.mycolObj.GetColumnHeightIncrement();
            if(CheckIfCanStack())
            {
                Stack();
                if(GameControl.instance.gameStatus == GameControl.GameStatus.GAME_FIRSTPIECE)
                    GameControl.instance.AfterCollisionAtGameStart();
            }
            else
                Fall();
        }
        else
        {
            Fall();
        }
    }

    void Stack()
    {
        stackStatus.isStackSuccessful = true;
        GameControl.instance.AfterPieceStackingSuccessfully(stackStatus.isDeadCenter);
        doTween.StackingNoDeadCenterAnimation(stackStatus.fallenSide, stackStatus.isDeadCenter);
        GameControl.instance.columnPiecesObj.Add(this.gameObject);
        //this.transform.GetComponent<StackAgent>().AddMinPosY();
    }

    void Fall()
    {
        stackStatus.isStackSuccessful = false;
        GameControl.instance.AfterPieceStackingFailed(stackStatus.fallenSide);
        doTween.FallenAnimation(stackStatus.fallenSide);
        transform.SetParent(GameControl.instance.piecePoolObj.idlePieceArea, true);
    }

    public bool CheckIfCanStack()
    {
        float absDeltaX = Mathf.Abs(GameControl.instance.mycolObj.deltaX);

        checkFallenSide();
        doTween.GetDeltaXFromCollision(absDeltaX);
       
        if(absDeltaX < stackRange)
        {
            isFallen = false;
            checkIfDeadCenter(absDeltaX, GameControl.instance.mycolObj.topPieceLocalPos.x, GameControl.instance.mycolObj.dropPieceCol);
            // Debug.Log(GameControl.instance.mycolObj.topPieceCol.gameObject.name + "  " + GameControl.instance.mycolObj.topPieceLocalPos.x + " | " 
            //     + GameControl.instance.mycolObj.dropPieceCol.gameObject.name + "  " + GameControl.instance.mycolObj.dropPieceLocalPos.x + " || " + "drop true", gameObject);
            return true;
        }
        else 
        {
            // Debug.Log(GameControl.instance.mycolObj.topPieceCol.gameObject.name + "  " + GameControl.instance.mycolObj.topPieceLocalPos.x + " | " 
            //     + GameControl.instance.mycolObj.dropPieceCol.gameObject.name + "  " + GameControl.instance.mycolObj.dropPieceLocalPos.x + " || " + "drop false", gameObject);
            isFallen = true;
            return false;
        }
    }

    private void checkIfDeadCenter(float absDeltaX, float topPiecePosX, Collider2D other)
    {
        if(absDeltaX < deadCenterRange)
        {
            stackStatus.isDeadCenter = true;
            Vector3 pos = other.transform.localPosition;
            pos.x = topPiecePosX;
            other.transform.localPosition = pos;
            pos = other.transform.position;
            pos.z -= 0.5f;
            pos.y -= 0.5f;
            GameControl.instance.particleObj.PlayStackDeadCenterAnim(pos);
            GameControl.instance.particleObj.PlayComboPeriodAnim();
        }
        else
        {
            stackStatus.isDeadCenter = false;
        }
    }

    private void checkFallenSide()
    {
        if(GameControl.instance.mycolObj.deltaX > 0)
        {
            stackStatus.fallenSide = 1;  // right side
        }
        else
        {
            stackStatus.fallenSide = -1;  // left side
        }
        // Debug.Log("fallen side = " + stackStatus.fallenSide);
    }

    void OnBecameInvisible()
    {
        if(isFallen)
            GameControl.instance.particleObj.PlayFallenWaterAnim(transform.position);
    }
}
