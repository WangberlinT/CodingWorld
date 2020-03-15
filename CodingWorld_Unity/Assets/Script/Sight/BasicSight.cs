using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeWorldInterface;
using Events;
public class BasicSight : MonoBehaviour, SightSubject
{
    public float view_radius = 8.0f;
    public float ray_num = 30;
    public float sight_angle = 90;

    private List<SightObserver> observers;
    private void Update()
    {
        DrawFiledOfView();
    }

    private void DrawFiledOfView()
    {
        List<GameObject> on_sight_objs;
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

                Debug.Log("find: "+hitt.collider.name);
            }

            Debug.DrawLine(transform.position, end_pos, Color.red);
        }
        
    }

    public void AddObserver(SightObserver s)
    {
        observers.Add(s);
    }

    public void NotifyObservers()
    {
        //todo 还没有写相关逻辑
        ScannedObjectEvent s = new ScannedObjectEvent();
        for(int i = 0;i < observers.Count;i ++)
        {
            observers[i].OnScannedObject(s);
        }
    }
}
