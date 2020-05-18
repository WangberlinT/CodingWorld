using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //------------Move Control Public-------------
    public float speed = 5;
    public float jump_speed = 10;
    public Transform highLightBlock;
    public Transform placeBlock;
    //------------View Control Public-------------
    public float sensitive = 180f;
    public float up_max = 90f;
    public float down_max = -90f;

    //------------Move Control Private-------------
    private Rigidbody rigidbody;
    private bool isJump = false;
    private float checkIncrement;

    //------------View Control Private-------------
    private Transform camTrans;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camTrans = transform.Find("Main Camera");
    }

    void FixedUpdate()
    {
        if (gameObject.GetComponent<ConflictControl>().notgamescene)
        {
            float moveY = 0;
            float moveX = Input.GetAxis("Horizontal") * speed;
            float moveZ = Input.GetAxis("Vertical") * speed;

            if (isJump)
                Jump();

            Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
            move *= Time.fixedDeltaTime;
            transform.position += move;
        }

    }

    void Update()
    {
        if (gameObject.GetComponent<ConflictControl>().notgamescene) { 
            ViewRotate();
            if (Input.GetButtonDown("Jump"))
                isJump = true;
        }
    }

    private void ViewRotate()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitive * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, down_max, up_max);
        rotationY += Input.GetAxis("Mouse X") * sensitive * Time.deltaTime;
        
        //rotate
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        camTrans.localEulerAngles = new Vector3(rotationX, 0, 0);
    }

    private void placeCursorBlocks()
    {
        //todo
        float step = checkIncrement;
        Vector3 lastPos = new Vector3();
    }

    void Jump()
    {
        rigidbody.velocity = new Vector3(0, jump_speed, 0);
        isJump = false;
    }
}
