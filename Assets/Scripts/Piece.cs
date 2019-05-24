using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

	public float downForce = 200f;
    public bool isHooked = false;
    public Vector3 pos;

	private Rigidbody2D rb2d;
	private bool isStacked = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // rb2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
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
    	isStacked = true;
    	rb2d.velocity = Vector2.zero;
    	GameControl.instance.PieceStacked();
    }
}
