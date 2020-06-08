using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureData
{
    public string name;
    public float[] position = new float[3];

    public CreatureData(Vector3 pos)
    {
        name = "Creature" + GameRecorder.creatureIndex;
        GameRecorder.creatureIndex++;
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
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
}
