using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public enum RotationAxis
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public float sensitive = 180f;
    public float up_max = 90f;
    public float down_max = -90f;
    public RotationAxis rotation = RotationAxis.MouseXandY;


    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ViewRotate();
    }

    void ViewRotate()
    {

        rotationX -= Input.GetAxis("Mouse Y")*sensitive*Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, down_max, up_max);
        rotationY += Input.GetAxis("Mouse X")*sensitive*Time.deltaTime;
        //rotate
        if(rotation == RotationAxis.MouseX)
        {
            transform.localEulerAngles = new Vector3(0, rotationY, 0);
        }
        else if (rotation == RotationAxis.MouseY)
        {
            transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}
