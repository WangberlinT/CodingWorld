using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class huanrao1 : Animal, SightObserver
{
    bool s = true;
    public huanrao1(BasicSight s, Movable m) : base(s, m)
    {
    }
    public huanrao1() { }
    public override IEnumerator Run()
    {
        Begin();
        yield return Task();
        End();
    }
    public override IEnumerator Task()
    {
        // write code here
       
while (true)
        {
            move.MoveTo(move.Forward(2), 2);
            yield return new WaitForSeconds(1.1f);
            move.MoveTo(move.Left(2), 2);
            yield return new WaitForSeconds(1.1f);
            move.MoveTo(move.Behind(2), 2);
            yield return new WaitForSeconds(1.1f);
            move.MoveTo(move.Right(2), 2);
            yield return new WaitForSeconds(1.1f);
        }
    }
    public void OnScannedObject(ScannedObjectEvent e)
    {
    }
}
