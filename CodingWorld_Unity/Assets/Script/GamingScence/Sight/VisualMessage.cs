﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
public class VisualMessage
{
    /*
     * 1. 绝对位置
     * 2. ControlObject
     */
    private Vector3 absolute_position;
    private ObjectType type;
    private ControlObject c_obj;
    private string name;
    public VisualMessage(Vector3 abs_pos,ControlObject c,ObjectType obj_type,string obj_name)
    {
        c_obj = c;
        type = obj_type;
        absolute_position = abs_pos;
        name = obj_name;
    }

    public ObjectType GetObjectType()
    {
        return type;
    }

    public Vector3 GetAbsPosition()
    {
        return absolute_position;
    }

    public void SetAbsPosition(Vector3 pos)
    {
        absolute_position = pos;
    }

    public string GetName()
    {
        return name;
    }
}
