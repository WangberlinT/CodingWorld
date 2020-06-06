using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class UserScript1 : Animal, SightObserver
{
    bool s = true;
    public UserScript1(BasicSight s, Movable m) : base(s, m)
    {

    }

    public UserScript1() { }

    public override IEnumerator Run()
    {
        Begin();
        yield return Task();
        End();
    }

    public override IEnumerator Task()
    {

        move.MoveTo(move.Left(3), 1);
        yield return new WaitForSeconds(3.1f);
        move.MoveTo(move.Right(3), 1);
        yield return new WaitForSeconds(3.1f);
        setMoveFinish();

    }

    public void OnScannedObject(ScannedObjectEvent e)
    {
        List<VisualMessage> messages = e.GetAllMessages();
        if(messages != null)
        {
            for (int i = 0; i < messages.Count; i++)
                Debug.Log(messages[i].GetName());
            s = false;
            move.Stop();
        }
        
    }

}
