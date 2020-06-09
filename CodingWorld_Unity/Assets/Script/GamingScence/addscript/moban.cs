﻿using System.Collections;
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
        move.Jump(300);
        yield return new WaitForSeconds(1f);

    }

    public void OnScannedObject(ScannedObjectEvent e)
    {

    }

}