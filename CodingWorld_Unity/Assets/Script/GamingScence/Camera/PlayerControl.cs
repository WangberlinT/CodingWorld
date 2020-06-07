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
    private int selectedBlockIndex = 1;
    private World world;
    private Text promptText;

    private bool cubeMode = true;


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
        if (GameObject.Find("Player").GetComponent<ConflictControl>().notgamescene)
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
        if (GameObject.Find("Player").GetComponent<ConflictControl>().notgamescene) { 
            
            
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
        //切换编辑模式
        if (Input.GetKeyDown(KeyCode.R))
            cubeMode = !cubeMode;

        float scrool = Input.GetAxis("Mouse ScrollWheel");

        if(cubeMode)
            UpdateIndex(world.blocktypes.Length, scrool);
        else
        {
            //TODO: coding obj select
            promptText.text = "Creature selected";
        }



        if (highLightBlock.gameObject.activeSelf)
        {
            debugScreen.LogMessage("here", "=======");
            if(Input.GetMouseButtonDown(0))
            {
                debugScreen.LogMessage("Click", "Left");
                if (cubeMode)
                    BreakBlock();
                else
                    BreakCreature();
            }
            else if(Input.GetMouseButtonDown(1))
            {
                debugScreen.LogMessage("Click", "Right");
                if (cubeMode)
                    CreateBlock();
                else
                    CreateCreature();
            }
            else
                debugScreen.LogMessage("Click", "None");
        }
    }

    private void UpdateIndex(int length,float scrool)
    {
        if (scrool != 0)
        {
            if (scrool > 0)
            {
                selectedBlockIndex = (selectedBlockIndex + 1) % length;
            }
            else
            {
                int complete = length - 1;
                selectedBlockIndex = (selectedBlockIndex + complete) % length;
            }

            if (cubeMode)
                promptText.text = world.blocktypes[selectedBlockIndex].blockName + " selected";
            else
                promptText.text = "CodingObject selected";
        }
    }

    private CubeData CreateCubeData(int index, Vector3 pos,bool creation)
    {
        return new CubeData(world.blocktypes[index].blockName, index, creation, pos);
    }

    private void CreateBlock()
    {
        //not air
        if(selectedBlockIndex != 0)
        {
            world.GetChunkFromVector3(placeBlock.position).EditVoxel(placeBlock.position, selectedBlockIndex);
            GameRecorder.GetInstance().UpdateCubes(CreateCubeData(selectedBlockIndex, placeBlock.position, true));
        }
            

    }
    private void BreakBlock()
    {
        Debug.Log("Break");
        world.GetChunkFromVector3(highLightBlock.position).EditVoxel(highLightBlock.position, 0);
        //index = air
        GameRecorder.GetInstance().UpdateCubes(CreateCubeData(0, highLightBlock.position, false));
    }

    private void BreakCreature()
    {
        Vector3 offset = new Vector3(0.5f, 0, 0.5f);
        world.DeleteCreature(highLightBlock.position + offset);
    }

    private void CreateCreature()
    {
        Vector3 offset = new Vector3(0.5f, 0, 0.5f);
        world.AddCreature(placeBlock.position + offset);
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
            Vector3 breakPos = endPos + ray.direction * step;
            breakPos.x = Mathf.FloorToInt(breakPos.x);
            breakPos.y = Mathf.FloorToInt(breakPos.y);
            breakPos.z = Mathf.FloorToInt(breakPos.z);

            highLightBlock.position = breakPos;
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
