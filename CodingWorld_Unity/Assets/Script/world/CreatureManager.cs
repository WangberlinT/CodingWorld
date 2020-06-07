using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureManager
{
    //保存着Monster的记录信息
    private Dictionary<Vector3, CreatureData> creatureDatas = new Dictionary<Vector3, CreatureData>();
    //保存着EnermySubject
    private Dictionary<Vector3, Creature> creatures = new Dictionary<Vector3, Creature>();
    private bool hasCreature;

    public CreatureManager()
    {
        hasCreature = false;

    }

    public void AddCreature(CreatureData creatureData)
    {
        creatureDatas.Add(creatureData.GetPos(),creatureData);
        CreatureFactory(creatureData, false);
    }

    public void DeleteCreature(Vector3 pos)
    {
        if (creatureDatas.ContainsKey(pos))
        {
            creatureDatas.Remove(pos);
            creatures[pos].Delete();
            creatures.Remove(pos);
        }
    }

    public List<CreatureData> GetCreatureDatas()
    {
        List<CreatureData> list = new List<CreatureData>();
        list.AddRange(creatureDatas.Values);
        return list;
    }

    public void LoadCreatureDatas(List<CreatureData> datas)
    {
        if(datas != null && datas.Count != 0)
        {
            hasCreature = true;
            foreach(CreatureData c in datas)
            {
                creatureDatas.Add(c.GetPos(), c);
            }
        }
    }

    public void GenerateCreatures()
    {
        if(hasCreature)
        {
            Debug.Log("creatureDatas size: " + creatureDatas.Values.Count);
            foreach (CreatureData c in creatureDatas.Values)
            {
                CreatureFactory(c, false);
            }
        }
    }

    public void DestoryCreatures()
    {
        if(creatures.Count != 0)
        {
            foreach(Creature c in creatures.Values)
            {
                c.Delete();
            }
            creatures.Clear();
        }
    }

    private void CreatureFactory(CreatureData data,bool isPause)
    {
        Debug.Log("Creature generate!" + data.GetPos());
        Creature tmp = new Creature(data);
        creatures[data.GetPos()] = tmp;
        tmp.SetPaused(isPause);
    }
}
