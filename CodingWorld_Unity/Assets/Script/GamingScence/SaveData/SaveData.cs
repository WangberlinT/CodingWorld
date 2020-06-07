using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
   public List<Hashtable> object_data = new List<Hashtable>();

    public void addNull()
    {
        Hashtable obj_data = new Hashtable();
        obj_data.Add("name", "empty");
        object_data.Add(obj_data);
    }

    public void add(string name, string script)
    {
        GameObject go = GameObject.Find(name);
        Hashtable obj_data = new Hashtable();
        obj_data.Add("name", name);
        obj_data.Add("script",script);
        obj_data.Add("runstate", go.GetComponent<ObjectManager>().runstate);
        object_data.Add(obj_data);
    }


}

public class JsonVector
{


    public static Vector3 ToVector3(string v)
    {
        float[] pos = new float[3];
        int i = 0;
        string[] axis = v.Split(' ');
        foreach(string s in axis)
        {
            pos[i] =float.Parse( s.Split(':')[1]);
            i++;
        }
        return new Vector3(pos[0], pos[1], pos[2]);
    }

    public static string Tostring(Vector3 v )
    {
        return string.Format("x:{0:F} y:{1:F} z:{2:F}", v.x,v.y,v.z);
        
    }
}


   

