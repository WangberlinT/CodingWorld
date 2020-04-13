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

        //读取当前运行路径
        string temp_dir = Directory.GetCurrentDirectory();
        string dll_dir = temp_dir + "\\CodingWorld_Data\\UserDll\\";
        Debug.Log(dll_dir);
        //Assembly assembly = Assembly.LoadFile(dll_dir + script + ".dll");
        
        //在编辑模式下使用下面的 TODO: 通过运行状态自动切换
        Assembly assembly = Assembly.LoadFile("D:\\Data of Wangberlin\\SweetyAndDoggy\\CodingWorld\\Release\\CodingWorld_Data\\UserDll\\" + script + ".dll");

        Type type = assembly.GetType(script);
        Debug.Log("AddAnimalScript!");
        Animal a =(Animal) assembly.CreateInstance(script);
        a.SetBasicSight(eye.GetComponent<BasicSight>());
        a.SetMovable(gameObject.GetComponent<Movable>());

        user = a;
        user.Begin();
        gameObject.AddComponent(type);
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
