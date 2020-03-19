using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
public class Animal : ControlObject
{
    ObjectType type = ObjectType.Animal;
    BasicSight sight;

    public Animal(BasicSight s)
    {
        sight = s;
    }

    public override void Run()
    {
        // 子类需要重载这个方法以实现行为
        Begin();
        for(int i = 0;i < 10;i ++)
            Task();
        End();
    }

    public override ObjectType GetObjectType()
    {
        return type;
    }


    public override void Task()
    {
        // 子类需要重载这个方法以实现行为
        Debug.Log("Animal! UP");
        sight.SightUP(30f);
        sight.SightDown(30f);
    }

}
