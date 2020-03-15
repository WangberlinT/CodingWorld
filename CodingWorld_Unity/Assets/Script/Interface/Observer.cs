using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
namespace CodeWorldInterface
{
    public interface SightObserver
    {
        public void OnScannedObject(ScannedObjectEvent e);
    }
        

    public interface SightSubject
    {
        public void AddObserver(SightObserver s);
        public void NotifyObservers();
    }
}

