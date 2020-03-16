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
            MoveTo(withAngle(45, 4), 4);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           head(2,0.5f);


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
        return transform.forward;
    }

    public override void head(float distance, float maxspeed)
    {
        
        Vector3 destn = transform.forward.normalized * distance;
        Debug.Log(rb.position + destn);
        MoveTo(rb.position + destn, distance,maxspeed);

    }

    private void MoveTo(Vector3 destn,float distance, float speed)
    {
       
        iTween.MoveTo(gameObject, destn, distance/speed);
    }

    public override void jump()
    {
        rb.AddForce(transform.up * 200);
    }

    public override void left(float distance, float maxspeed)
    {
       
        Vector3 destn =  -1*transform.right.normalized * distance;
       
        MoveTo(rb.position + destn, distance, maxspeed);


    }

    public override void right(float distance, float maxspeed)
    {
        Vector3 destn = transform.right.normalized * distance;
       
        MoveTo(rb.position + destn, distance, maxspeed);

    }

    public override void tail(float distance, float maxspeed)
    {
        Vector3 destn = -1* transform.forward.normalized * distance;
        
        MoveTo(rb.position + destn, distance, maxspeed);

    }

    public override void turnLeft(float degree)
    {
        iTween.RotateAdd(gameObject, new Vector3(0, -1*degree, 0), 1);

    }
    
    public override void turnRight(float degree)
    {
        iTween.RotateAdd(gameObject, new Vector3(0, degree, 0), 1);
    }

    public Vector3 Forward(float distance)
    {
        return Forward(rb.position, distance);
    }

    public Vector3 Forward(Vector3 startposition, float distance)
    {
        return startposition + transform.forward.normalized * distance;
    }


    public Vector3 Left(float distance)
    {
        return Left(rb.position, distance);
    }

    public Vector3 Left(Vector3 startposition, float distance)
    {
        return startposition + transform.right.normalized * distance*-1;
    }

    public Vector3 Right(float distance)
    {
        return Right(rb.position, distance);
    }

    public Vector3 Right(Vector3 startposition, float distance)
    {
        return startposition + transform.right.normalized * distance;
    }
    public Vector3 Behind(float distance)
    {
        return Behind(rb.position, distance);
    }

    public Vector3 Behind(Vector3 startposition, float distance)
    {
        return startposition + transform.forward.normalized * distance*-1;
    }
    
    public void MoveTo(Vector3 position, float speed)
    {
        float dis = Vector3.Distance(rb.position, position);
        iTween.MoveTo(gameObject, position,dis/speed);
    }

    public Vector3 withAngle(float angle, float distance)
    {
        return withAngle(rb.position, angle, distance);
    }

    public Vector3 withAngle(Vector3 startposition, float angle, float distance)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, transform.up); ;
        Vector3 dir = rotation * transform.forward;
        return startposition + dir.normalized * distance;
    }
}
