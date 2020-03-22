using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class followScript : Animal, SightObserver
{
    bool s = true;
    public followScript(BasicSight s, Movable m) : base(s, m)
    {
    }
    public followScript() { }
    public override IEnumerator Run()
    {
        Begin();
        yield return Task();
        End();
    }
    public override IEnumerator Task()
    {
        Debug.Log("Animal! UP");
        sight.SightUP(30f);
        yield return new WaitForSeconds(1);
        sight.SightDown(30f);
        yield return new WaitForSeconds(1);
        while (true)
        {
            move.follow("followman");
            yield return new WaitForFixedUpdate();
        }
    }
    public void OnScannedObject(ScannedObjectEvent e)
    {
    }
}
