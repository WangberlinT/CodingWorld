using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //------------Move Control Public-------------
    public float speed = 5;
    public float jump_speed = 10;

    //------------Build Block Public------------
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
    public DebugScreen debugScreen;

    //------------Build Block-------------------------
    private int selectedBlockIndex = 0;
    private World world;
    private Text promptText;
    private Vector3 breakSelected = Vector3.negativeInfinity;


    private const string TAG = "PlayerControl";
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        camTrans = transform.Find("Main Camera");
        world = GameObject.Find("World").GetComponent<World>();
        promptText = GameObject.Find("Selection prompt").GetComponent<Text>();
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
            
            
            placeCursorBlocks();
            GetPlayerInput();
            ViewRotate();
        }
    }

    private void GetPlayerInput()
    {
        //TODO: Input
        if (Input.GetButtonDown("Jump"))
            isJump = true;

        float scrool = Input.GetAxis("Mouse ScrollWheel");

        if(scrool != 0)
        {
            if(scrool > 0)
            {
                selectedBlockIndex = (selectedBlockIndex + 1)%world.blocktypes.Length;
            }
            else
            {
                int complete = world.blocktypes.Length - 1;
                selectedBlockIndex = (selectedBlockIndex + complete)%world.blocktypes.Length;
            }

            promptText.text = world.blocktypes[selectedBlockIndex].blockName + " selected";
        }

        if(highLightBlock.gameObject.activeSelf)
        {
            if(Input.GetMouseButton(0))
            {
                debugScreen.LogMessage("Click", "Left");
                BreakBlock();
            }
            else if(Input.GetMouseButton(1))
            {
                debugScreen.LogMessage("Click", "Right");
                CreateBlock();
            }
            else
                debugScreen.LogMessage("Click", "None");
        }
    }
    private void CreateBlock()
    {
        world.GetChunkFromVector3(placeBlock.position).EditVoxel(placeBlock.position, selectedBlockIndex);
    }
    private void BreakBlock()
    {
        if(breakSelected != Vector3.negativeInfinity)
            world.GetChunkFromVector3(breakSelected).EditVoxel(breakSelected, 0);
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
        //clear
        breakSelected = Vector3.negativeInfinity;
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
        breakSelected = endPos;
    }

    void Jump()
    {
        rigidbody.velocity = new Vector3(0, jump_speed, 0);
        isJump = false;
    }
}
