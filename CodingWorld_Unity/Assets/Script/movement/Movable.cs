using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movable : BasicMovement
{
    private Rigidbody rb;
    private Vector3 startpos;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           head(2,0.5f);
            left(2, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tail(2, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            left(2, 0.5f);
                
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            right(2, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            turnLeft(20);


        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            turnRight(30);

        }


    }

    public Vector3 getHeading()
    {
        return rb.transform.forward;
    }

    public override void head(float distance, float maxspeed)
    {
        Vector3 destn = rb.transform.forward.normalized * distance;
        MoveToPosition(destn,distance,maxspeed);

    }

    private void MoveToPosition(Vector3 destn,float distance, float maxspeed)
    {
       
        iTween.MoveAdd(gameObject, destn, distance/maxspeed);
    }

    public override void jump()
    {
        rb.AddForce(rb.transform.up * 200);
    }

    public override void left(float distance, float maxspeed)
    {
       
        Vector3 destn =  -1*rb.transform.right.normalized * distance;
       
        MoveToPosition(destn, distance, maxspeed);


    }

    public override void right(float distance, float maxspeed)
    {
        Vector3 destn = rb.transform.right.normalized * distance;
       
        MoveToPosition(destn, distance, maxspeed);

    }

    public override void tail(float distance, float maxspeed)
    {
        Vector3 destn = -1* rb.transform.forward.normalized * distance;
        
        MoveToPosition(destn, distance, maxspeed);

    }

    public override void turnLeft(float degree)
    {

        iTween.RotateAdd(gameObject, new Vector3(0,-1* degree, 0), 1);

    }
    
    public override void turnRight(float degree)
    {
        iTween.RotateAdd(gameObject, new Vector3(0, degree, 0), 1);
    }
}
