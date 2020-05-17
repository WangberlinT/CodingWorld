using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;



public class Movable : BasicMovement
{
    private Rigidbody rb;
    private Vector3 startpos;
    private float height;
    private Vector3 direction;
    private float dis_to_ground;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        
        height = transform.localScale.y/2;
    }

    private void Update()
    {
        
        groundcheck();
        //Debug.Log(dis_to_ground+" groud");
        if (this.transform.up != new Vector3(0, 1, 0)) {
            Debug.Log(1);
            this.transform.localEulerAngles=direction;
        }
        else
        {
            direction = this.transform.localEulerAngles;
        }
        

    }

    public void setGravity(bool b)
    {
        rb.useGravity = b;
    }

    public Vector3 getHeading()
    {
        return transform.forward;
    }

    public override void head(float distance, float maxspeed)
    {

        Vector3 destn = transform.forward.normalized * distance;
        MoveTo(rb.position + destn, distance, maxspeed);

    }

    private void MoveTo(Vector3 destn, float distance, float speed)
    {

        MoveTo(destn, speed);
    }

    public override void jump()
    {
        rb.AddForce(transform.up * 200);
    }

    public override void left(float distance, float maxspeed)
    {

        Vector3 destn = -1 * transform.right.normalized * distance;

        MoveTo(rb.position + destn, distance, maxspeed);


    }

    public override void right(float distance, float maxspeed)
    {
        Vector3 destn = transform.right.normalized * distance;

        MoveTo(rb.position + destn, distance, maxspeed);

    }

    public override void tail(float distance, float maxspeed)
    {
        Vector3 destn = -1 * transform.forward.normalized * distance;

        MoveTo(rb.position + destn, distance, maxspeed);

    }

    public override void turnLeft(float degree)
    {
        iTween.RotateAdd(gameObject, new Vector3(0, -1 * degree, 0), 1);

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
        return startposition + transform.right.normalized * distance * -1;
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
        return startposition + transform.forward.normalized * distance * -1;
    }

    public void MoveTo(Vector3 position, float speed)
    {
       
            iTween.MoveTo(gameObject, iTween.Hash("position", position,
                                                    "speed", speed
                                                    ));
        
    }

    public void MoveToR(Vector3 position, float speed)
    {

        iTween.MoveTo(gameObject, iTween.Hash("position", position,
                                                "speed", speed,
                                                "orienttopath", true
                                                ));
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



    public void MovePath(Vector3[] path, float speed)
    {
        Hashtable arg = new Hashtable();
        arg.Add("path", path);
        arg.Add("movetopath", true);
        arg.Add("orienttopath", true);
        arg.Add("speed", speed);
        iTween.MoveTo(gameObject, arg);

    }

    public void MoveLoop(Vector3[] path, float speed)
    {
        Hashtable arg = new Hashtable();
        arg.Add("path", path);
        arg.Add("movetopath", true);
        arg.Add("orienttopath", true);
        arg.Add("speed", speed);
        arg.Add("loopType", iTween.LoopType.pingPong);
        iTween.MoveTo(gameObject, arg);
        Debug.Log("try to loop");
    }

    public void follow(GameObject go)
    {
        Hashtable ht = new Hashtable();
        Vector3 vc = (go.transform.position - rb.position).normalized;
        Vector3 aimpos = go.transform.position - vc * 3;
        aimpos.y = 0.5f;
        ht.Add("position", aimpos);
        ht.Add("speed", 5f);
        iTween.MoveUpdate(gameObject, ht);
    }

    public void follow(string objectname)
    {
        GameObject go = GameObject.Find(objectname);
        follow(go);
    }
    public void jumpForward() 
    {
        rb.AddForce(new Vector3(this.transform.forward.x,2* Vector3.up.y, this.transform.forward.z) * 200f);
    }

    public void Stop()
    {
        iTween.Stop(gameObject);
    }
    bool groundcheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))//Physics.Raycast(射线发出位置，射线方向，射线长度）
        {
            dis_to_ground = hit.distance-height;
            return true;
        }
        else
            return false;
    }
   
    float groundcheck(Vector3 desposition)
    {
        RaycastHit hit;
        if (Physics.Raycast(desposition, transform.TransformDirection(Vector3.down), out hit))//Physics.Raycast(射线发出位置，射线方向，射线长度）
        {
           
            return hit.distance - height;
        }
        else
            return -1;
    }

}
