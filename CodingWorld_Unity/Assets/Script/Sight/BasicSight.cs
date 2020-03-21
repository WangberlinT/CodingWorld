using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeWorldInterface;
using Events;

/*
 * BasicSight
 * 基本视觉处理接口
 */
public class BasicSight : MonoBehaviour, SightSubject
{
    public float view_radius = 8.0f;
    public float ray_num = 30;
    public float sight_angle = 90;
    public float X_min = -45f;
    public float X_max = 45f;
    public float rotate_speed = 90f;//每秒旋转90度

    private List<SightObserver> observers = new List<SightObserver>();
    private List<Collider> sight_colliders = new List<Collider>();
    private Vector3 aim_rot;//记录期望旋转的角度(Local Coordinate)
    private bool enable_rotate;
    private Vector3 currentAngle = Vector3.zero;
    private const float ROUND = 360f;

    void Start()
    {
        aim_rot = Vector3.zero;
        enable_rotate = false;
        Debug.Log("Sight Start");
    }

    private void Update()
    {
        DrawFiledOfView();
        if(enable_rotate)
            Rotate();
    }

    //----------User Interface----------------
    public void SightRight(float degree)
    {
        if (enable_rotate)
            return;
        aim_rot.y = (degree);
        enable_rotate = true;
    }

    public void SightLeft(float degree)
    {
        SightRight(-degree);
        enable_rotate = true;
    }

    public void SightUP(float degree)
    {
        // 最大只能朝上xmin 度
        aim_rot.x = -degree;
        if (aim_rot.x < X_min)
            aim_rot.x = X_min;

        enable_rotate = true;
    }

    public void SightDown(float degree)
    {
        // 最大只能朝下down_max 度
        aim_rot.x = degree;
        if (aim_rot.x > X_max)
            aim_rot.x = X_max;

        enable_rotate = true;
    }

    public void SightLookAt(Vector3 direction)
    {
        //基本测试成功
        Vector3 proj_z = Vector3.Project(direction, Vector3.forward);
        Vector3 proj_y = Vector3.Project(direction, Vector3.up);
        Vector3 proj_x = Vector3.Project(direction, Vector3.right);

        Vector3 xz_v = proj_z + proj_x;
        Vector3 yz_v = proj_z + proj_y;

        float Rotate_Y = Vector3.Angle(Vector3.forward, xz_v);
        float Rotate_X = -1*Vector3.Angle(Vector3.forward, yz_v);

        aim_rot.x = Rotate_X;
        aim_rot.y = Rotate_Y;

        enable_rotate = true;

    }

    //----------------------------------------

    

    private void Rotate()
    {
        //第一版实现,可能有顺序问题,解决XY独立旋转问题
        
        //Debug.Log("temp transAngle = " + string.Format("({0},{1},{2})", transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
        if (aim_rot == Vector3.zero)
        {
            enable_rotate = false;
            //Debug.Log("temp aimAngle = " + string.Format("({0},{1},{2})", aim_rot.x, aim_rot.y, aim_rot.z));
            //Debug.Log("temp transAngle = " + string.Format("({0},{1},{2})", transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
            return;
        }

        float Max_Rotate = Time.deltaTime * rotate_speed;
        Vector3 temp_rotate = Vector3.zero;
        if(Mathf.Abs(aim_rot.x) > Max_Rotate)
        {
            temp_rotate.x = Max_Rotate * aim_rot.x / Mathf.Abs(aim_rot.x);
        }
        else
        {
            temp_rotate.x = aim_rot.x;
        }
        transform.Rotate(temp_rotate.x, 0, 0,Space.Self);
        if (Mathf.Abs(aim_rot.y) > Max_Rotate)
        {
            temp_rotate.y = Max_Rotate * aim_rot.y / Mathf.Abs(aim_rot.y);
        }
        else
        {
            temp_rotate.y = aim_rot.y;
        }
        aim_rot = aim_rot - temp_rotate;
        currentAngle += temp_rotate;
        currentAngle.x = Mathf.Clamp(currentAngle.x, X_min, X_max);
        transform.localEulerAngles = currentAngle;
    }

    private void DrawFiledOfView()
    {
        sight_colliders.Clear();
        Dictionary<Collider, bool> sight_objs = new Dictionary<Collider, bool>();
        float angle_step = sight_angle / ray_num;
        Vector3 sight_left = Quaternion.AngleAxis(-sight_angle/2,transform.up) * transform.forward * view_radius;
        //共有ray_num条射线
        for(int i = 0; i <= ray_num;i ++)
        {
            Vector3 v = Quaternion.AngleAxis(angle_step*i, transform.up) * sight_left;
            //创建射线
            Ray ray = new Ray(transform.position, v);
            RaycastHit hitt = new RaycastHit();
            Physics.Raycast(ray, out hitt, view_radius);

            Vector3 end_pos = transform.position + v;

            if(hitt.transform != null)
            {
                Collider temp = hitt.collider;
                if (!sight_objs.ContainsKey(temp))
                {
                    sight_objs.Add(temp, true);
                    sight_colliders.Add(temp);
                }
                    
                end_pos = hitt.point;
            }

            Debug.DrawLine(transform.position, end_pos, Color.red);
        }

        if (sight_colliders.Count != 0)
            NotifyObservers();
        
    }

    public void AddObserver(SightObserver s)
    {
        observers.Add(s);
    }

    public void NotifyObservers()
    {
        ScannedObjectEvent s = new ScannedObjectEvent(sight_colliders);
        for(int i = 0;i < observers.Count;i ++)
        {
            observers[i].OnScannedObject(s);
        }
    }
}
