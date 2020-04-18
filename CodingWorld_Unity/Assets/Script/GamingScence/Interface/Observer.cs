using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
namespace CodeWorldInterface
{
    public interface SightObserver
    {
        void OnScannedObject(ScannedObjectEvent e);
    }
        

    public interface SightSubject
    {
        void AddObserver(SightObserver s);
        void NotifyObservers();
    }
}

