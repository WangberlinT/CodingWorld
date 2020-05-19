using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class moban : Animal, SightObserver
{

    public moban(BasicSight s, Movable m) : base(s, m)
    {

    }

    public moban() { }

    public override IEnumerator Run()
    {
        Begin();
        yield return Task();
        End();
    }

    public override IEnumerator Task()
    {
        // write code here
        move.MoveTo(move.Left(4), 2);
        yield return new WaitForSeconds(2.1f);
        move.MoveTo(move.Right(4), 2);
        yield return new WaitForSeconds(2.1f);

    }

    public void OnScannedObject(ScannedObjectEvent e)
    {

    }

}