using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSight : MonoBehaviour
{
    public float view_radius = 8.0f;
    public float ray_num = 30;
    public float sight_angle = 90;
    private void Update()
    {
        DrawFiledOfView();
    }

    private void DrawFiledOfView()
    {
        float angle_step = sight_angle / ray_num;
        Vector3 sight_left = Quaternion.Euler(0, -sight_angle/2, 0) * transform.forward * view_radius;
        //共有ray_num条射线
        for(int i = 0; i <= ray_num;i ++)
        {
            Vector3 v = Quaternion.Euler(0, angle_step*i, 0) * sight_left;
            //创建射线
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();

            Physics.Raycast(ray, out hitt, view_radius);

            Vector3 end_pos = transform.position + v;
            if(hitt.transform != null)//hitt是什么
            {
                end_pos = hitt.point;
            }

            Debug.DrawLine(transform.position, end_pos, Color.red);
        }
        
    }
}
