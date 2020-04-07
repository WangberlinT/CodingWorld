using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class abc : Animal, SightObserver
{
    bool s = true;
    public abc(BasicSight s, Movable m) : base(s, m)
    {
          
    }
    public abc() { }
    public override IEnumerator Run()
    {
        Begin();
        yield return Task();
        End();
    }
    public override IEnumerator Task()
    {
        Debug.Log("Script1");
        sight.SightUP(30f);
        yield return new WaitForSeconds(1);
        sight.SightDown(30f);
        yield return new WaitForSeconds(1);
        while (true)
        {
            move.MoveTo(move.Forward(2), 2);
            yield return new WaitForSeconds(1.1f);
            move.MoveTo(move.Left(2), 2);
            yield return new WaitForSeconds(1.1f);
        }
    }
    public void OnScannedObject(ScannedObjectEvent e)
    {
    }
}
