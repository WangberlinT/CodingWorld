using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;

/*
 * Environment 基础类，默认所有ControlObject方块初始化为Environment
 */
public class Environment : ControlObject
{
    protected ObjectType type = ObjectType.Enviorment;
    public override ObjectType GetObjectType()
    {
        return type;
    }

    public override IEnumerator Run()
    {
        return null;
    }

    public override IEnumerator Task()
    {
        return null;
    }
}
