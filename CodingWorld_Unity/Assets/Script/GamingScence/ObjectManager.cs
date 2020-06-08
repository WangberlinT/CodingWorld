using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

using System.IO;
using System.Reflection;
using System;

public class ObjectManager : MonoBehaviour
{
    public ControlObject user;
    public GameObject eye;
    private VisualMessage visual;
    FileStream fs;
    public bool runstate { get; set; }
    void Start()
    {
        //初始化ControlObject 部件,Visual
        if (eye != null)
        {
            Debug.Log("Animal");
            //addAnimalScript("UserScript1");
            BasicSight sight = eye.GetComponent<BasicSight>();
            Movable move = GetComponent<Movable>();
            if (user == null)
                user = new Animal(sight, move);
        }
        else
            user = new Environment();

        visual = new VisualMessage(transform.position, user, user.GetObjectType(),gameObject.name);
        Debug.Log("ObjectManager"+visual.GetName()+" Init OK");
        runstate = true;
        if (GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().scriptRelation.ContainsKey(gameObject.name))
        {
            

            addAnimalScript((string)GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().scriptRelation[gameObject.name]);
            
        }
        foreach(CreatureData data in GameRecorder.GetInstance().GetCreatureDatas())
        {
            if (data.GetName().Equals(gameObject.name))
            {
                gameObject.transform.position = data.GetPos();
                break;
            }
        }
            
        //开始ControlObject任务
        user.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        //开始ControlObject任务
        if(user != null)
        {
            if (user.IsRunning())
                StartCoroutine(userrun());
            user.End();
        }
        UpdateVisualMessage();
    }

    public void addAnimalScript(string script)
    {
        string userDllpath = "";
#if UNITY_EDITOR
        userDllpath = "F:\\BaiduNetdiskDownload\\Majiang\\example\\CodingWorld_Data\\UserDll\\";
#else
        userDllpath=".\\CodingWorld_Data\\UserDll\\";
#endif
        if (!Directory.Exists(userDllpath)) Directory.CreateDirectory(userDllpath);
        Debug.Log(userDllpath);
        fs = new FileStream(userDllpath + script + ".dll", FileMode.OpenOrCreate);

        byte[] b = new byte[fs.Length];

        fs.Read(b, 0, b.Length);

        fs.Dispose();

        fs.Close();

        Assembly assembly = Assembly.LoadFile(userDllpath + script + ".dll");

        Type type = assembly.GetType(script);
        Debug.Log(type);
        Debug.Log("AddAnimalScript!");
        Animal a =(Animal) assembly.CreateInstance(script);
        a.SetBasicSight(eye.GetComponent<BasicSight>());
        a.SetMovable(gameObject.GetComponent<Movable>());

        user = a;
        user.Begin();
        gameObject.AddComponent(type);
        Debug.Log("add Finished");
    }

    public VisualMessage GetVisualMessage()
    {
        return visual;
    }

    private void UpdateVisualMessage()
    {
        visual.SetAbsPosition(transform.position);
    }

    IEnumerator userrun()
    {
       yield return user.Run();
        
    }
}
