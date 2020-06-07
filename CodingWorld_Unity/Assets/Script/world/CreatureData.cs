using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreatureData
{
    public string name;
    public float[] position = new float[3];
    public int index;

    public CreatureData(Creature creature)
    {
        index = Creature.index;
        name = creature.GetName();
        Vector3 pos = creature.GetCreature().transform.position;
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
    }

    public Vector3 GetPos()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
}
