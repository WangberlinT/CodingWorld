using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 测试用，movement模块完成后删除
 */
public class VisualControl : MonoBehaviour
{
    public BasicSight sight;
    public float rotate_speed = 30f;
    void Start()
    {
        Debug.Log("ok!");
    }

    // Update is called once per frame
    void Update()
    {
        float horizon = Input.GetAxis("Horizontal");
        bool button = Input.GetButtonDown("Jump");
        float vertical = Input.GetAxis("Vertical");
        if(horizon!=0)
            sight.SightRight(horizon);
        

        if (button)
            sight.SightLookAt(new Vector3(1,0,0));
        if (vertical != 0)
            sight.SightUP(vertical);

    }
}
