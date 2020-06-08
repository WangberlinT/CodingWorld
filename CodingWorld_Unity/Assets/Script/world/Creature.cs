using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    private string name;
    private GameObject creature;
    private Animator creatureAni;

    private bool isPaused = false;    // 是否禁用
    private bool isDead = false;      //是否死亡
    private bool isMoving = false;   // 正在移动
    private bool isRotating = false; // 正在旋转

    private int faceIndex = 0;           //朝向下标 0为向前，1为向右，2为向后，3为向左

    public Creature(CreatureData data)
    {
        creature = GameObject.Instantiate((GameObject)Resources.Load("Prefab/Slime", typeof(GameObject)));
        creatureAni = creature.GetComponent<Animator>();
        creature.transform.position = data.GetPos();
        creature.transform.rotation = GameObject.Find("Player").transform.rotation;
        //creature.transform.up = new Vector3(0, 1, 0);
        name = data.GetName();
        creature.name = name;
    }

    public string GetName()
    {
        return name;
    }

    public GameObject GetCreature()
    {
        return creature;
    }

    public void SetFace(int face)
    {
        faceIndex = face;
    }

    public int GetFace()
    {
        return faceIndex;
    }

    public void Rotate()
    {
        creature.transform.rotation = Quaternion.Euler(new Vector3(0, 90 * (faceIndex), 0));
    }

    public bool GetPaused()
    {
        return isPaused;
    }
    public void SetPaused(bool pause)
    {
        isPaused = pause;
    }

    public bool GetMoving()
    {
        return isMoving;
    }
    public void SetMoving(bool moving)
    {
        isMoving = moving;
    }

    public void Delete()
    {
        Object.Destroy(creature);
    }

}
