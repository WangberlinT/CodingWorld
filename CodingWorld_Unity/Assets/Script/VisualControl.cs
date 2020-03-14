using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualControl : MonoBehaviour
{
    public float rotate_speed = 30f;
    void Start()
    {
        Debug.Log("ok!");
    }

    // Update is called once per frame
    void Update()
    {
        float horizon = Input.GetAxis("Horizontal");
        if(horizon != 0)
        {
            transform.Rotate(0, horizon * rotate_speed * Time.deltaTime, 0);
        }

    }
}
