using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Control;
using CodeWorldInterface;
using Events;
public class lookstop : Animal, SightObserver
{
    bool s = true;
    public lookstop(BasicSight s, Movable m) : base(s, m)
    {
    }
    public lookstop() { }
    public override IEnumerator Run()
    {
        Begin();
        yield return Task();
        End();
    }
public override IEnumerator Task()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            move.MoveTo(move.Forward(2),2);
            if (s == false)
                break;
            yield return new WaitForSeconds(1.1f);
        }
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
