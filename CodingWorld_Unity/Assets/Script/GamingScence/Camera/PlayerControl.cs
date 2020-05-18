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

    //------------Selection Control Public----------------
    public float selectDistance = 5f;
    public LayerMask mark;

    //------------Move Control Private-------------
    private Rigidbody rigidbody;
    private bool isJump = false;
    private float checkIncrement = 0.5f;

    //------------View Control Private-------------
    private Transform camTrans;
    private float rotationX = 0f;
    private float rotationY = 0f;

    //------------Debug Screen-------------------------
    private DebugScreen debugScreen;


    private const string TAG = "PlayerControl";
    

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camTrans = transform.Find("Main Camera");
        debugScreen = GameObject.Find("Debug Screen").GetComponent<DebugScreen>();
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
            placeCursorBlocks();
        }
    }

    private void GetPlayerInput()
    {
        //TODO: Input
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
        Ray ray = new Ray(camTrans.position, camTrans.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction*selectDistance,Color.red);

        

        RaycastHit hitt = new RaycastHit();
        Physics.Raycast(ray, out hitt, selectDistance);

        Vector3 endPos = ray.origin + ray.direction * selectDistance;

        if (hitt.transform != null)
        {
            endPos = hitt.point;

            Vector3 placePos = endPos - ray.direction * step;
            placePos.x = Mathf.FloorToInt(placePos.x);
            placePos.y = Mathf.FloorToInt(placePos.y);
            placePos.z = Mathf.FloorToInt(placePos.z);

            highLightBlock.position = placePos;
            placeBlock.position = placePos;

            highLightBlock.gameObject.SetActive(true);
            placeBlock.gameObject.SetActive(true);
        }
        else
        {
            highLightBlock.gameObject.SetActive(false);
            placeBlock.gameObject.SetActive(false);
        }
        string debugMessage = string.Format("End point: {0}", endPos);
        debugScreen.LogMessage(TAG, debugMessage);
        Debug.DrawLine(ray.origin, endPos, Color.red);

    }

    void Jump()
    {
        rigidbody.velocity = new Vector3(0, jump_speed, 0);
        isJump = false;
    }
}
