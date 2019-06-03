using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public bool isHooked = false;
    public bool isStacked = true;
    private bool stackStatus = true;
    public float angle = 90;

	private Rigidbody2D rb2d;

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
	        if(Input.GetKeyDown("space"))
	        {
	        	transform.parent = null;
                Vector3 p = transform.position;
                p.z = 0;
                transform.position = p;
	        	transform.rotation = Quaternion.Euler(0,0,0);
	        	rb2d.isKinematic = false;
	        	isHooked = false;
	        }
    	}
    }

    void OnCollisionEnter2D()
    {
        if(!isStacked)
        {
            GameControl.instance.OnPieceStacking();
            rb2d.isKinematic = true;
            rb2d.velocity = Vector3.zero;
            parent2Column();        
        }
    }

    void parent2Column()
    {
        transform.SetParent(GameControl.instance.columnObj.transform, true);
        // set the rotation for subObject using this way
        transform.localEulerAngles = Vector3.zero;
        rb2d.angularVelocity = 0;
    }

    void OnCollisionExit2D(Collision2D ctl)
    {
        if(!isStacked && ctl.collider.gameObject.tag == "Piece")
        {
            if(checkIfCanStack(ctl))
            {
                stackStatus = true;
                GameControl.instance.AfterPieceStackingSuccessfully();
            }
            else
            {
                stackStatus = false;
                GameControl.instance.AfterPieceStackingFailed();
                transform.position = new Vector3(0, -10f, 0);
            }
        }
        isStacked = true;
    }

    private bool checkIfCanStack(Collision2D ctl)
    {

        if(Mathf.Abs(ctl.collider.transform.localPosition.x - ctl.otherCollider.transform.localPosition.x) < 0.5)
        {
            Debug.Log(ctl.collider.gameObject.name + "  " + ctl.collider.transform.localPosition.x + " | " 
                + ctl.otherCollider.gameObject.name + "  " + ctl.otherCollider.transform.localPosition.x + " || " + "drop true");
            return true;
        }
        else 
        {
            Debug.Log(ctl.collider.gameObject.name + "  " + ctl.collider.transform.localPosition.x + " | " 
                + ctl.otherCollider.gameObject.name + "  " + ctl.otherCollider.transform.localPosition.x + " || " + "drop false");
            return false;
        }
    }



}
