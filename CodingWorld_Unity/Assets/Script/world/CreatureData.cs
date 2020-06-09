using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureData
{
    public string name;
    public float[] position = new float[3];
    public float[] rotation = new float[3];

    public CreatureData(Vector3 pos)
    {
        name = "Creature" + GameRecorder.creatureIndex;
        GameRecorder.creatureIndex++;
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
    }
    public CreatureData(Vector3 pos, Vector3 rot)
    {
        name = "Creature" + GameRecorder.creatureIndex;
        GameRecorder.creatureIndex++;
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
        rotation[0] = rot.x;
        rotation[1] = rot.y;
        rotation[2] = rot.z;
    }

    public string GetName()
    {
        return name;
    }

    public Vector3 GetPos()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    public void SetPos(Vector3 v)
    {
        position[0] = v.x;
        position[1] = v.y;
        position[2] = v.z;
    }

    public void SetRot(Vector3 v)
    {
        rotation[0] = v.x;
        rotation[1] = v.y;
        rotation[2] = v.z;
    }
    public Vector3 GetRot()
    {
        return new Vector3(rotation[0], rotation[1], rotation[2]);
    }
}
