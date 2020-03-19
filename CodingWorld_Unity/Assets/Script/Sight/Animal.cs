using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class Animal : ControlObject,SightObserver
{
    ObjectType type = ObjectType.Animal;
    BasicSight sight;
    Movable move;
    bool s=true;

    public Animal(BasicSight s, Movable m)
    {
        sight = s;
        move = m;
        sight.AddObserver(this);
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
        Debug.Log("Animal! UP");
        sight.SightUP(30f);
        yield return new WaitForSeconds(1);
        sight.SightDown(30f);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 5&&s; i++)
        {
            move.MoveTo(move.Forward(2), 2);
            yield return new WaitForSeconds(1.1f);
            move.MoveTo(move.Left(2), 2);
            yield return new WaitForSeconds(1.1f);
        }
 
    }

    public void OnScannedObject(ScannedObjectEvent e)
    {
        s = false;
        move.Stop();
    }
}
