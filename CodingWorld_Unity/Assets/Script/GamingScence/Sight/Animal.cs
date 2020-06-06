using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class Animal : ControlObject,SightObserver
{
    protected ObjectType type = ObjectType.Animal;
    protected BasicSight sight;
    protected Movable move;

    public Animal(BasicSight s, Movable m)
    {
        sight = s;
        move = m;
        sight.AddObserver(this);
    }

    public Animal() { }

    public void SetBasicSight(BasicSight b)
    {
        sight = b;
        sight.AddObserver(this);
    }

    public void SetMovable(Movable m)
    {
        move = m;
    }

    public override IEnumerator Run()
    {
        // 子类需要重载这个方法以实现行为
        Begin();
        yield return Task();
        End();
    }

    public override ObjectType GetObjectType()
    {
        return type;
    }

   

    public override IEnumerator Task()
    {
        // 子类需要重载这个方法以实现行为
        yield return null;
 
    }

    public void OnScannedObject(ScannedObjectEvent e)
    {

    }

    public void setMoveFinish()
    {
        move.gameObject.GetComponent<ObjectManager>().runstate = false;
    }

}
