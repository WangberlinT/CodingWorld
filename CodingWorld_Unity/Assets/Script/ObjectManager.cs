using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using System.Reflection;

public class ObjectManager : MonoBehaviour
{
    public ControlObject user;
    public GameObject eye;
    private VisualMessage visual;
    
    void Start()
    {
        //初始化ControlObject 部件,Visual
        BasicSight sight = eye.GetComponent<BasicSight>();
        Movable move = GetComponent<Movable>();
        if (user == null)
            user = new Animal(sight,move);
        visual = new VisualMessage(transform.position, user, user.GetObjectType(),gameObject.name);
        Debug.Log("ObjectManager Init OK");

        //开始ControlObject任务
        user.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        //开始ControlObject任务

        if (user.IsRunning())
            StartCoroutine(userrun());
        user.End();
        UpdateVisualMessage();
    }

    public void addAnimalScript(string classname)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        object o = asm.CreateInstance(classname);
        ConstructorInfo[] constructors = o.GetType().GetConstructors();

        //todo 反射
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
