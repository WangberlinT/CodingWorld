using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CodingWorldUtil
{
    public class StaticUtil
    {
        public static GameObject GetRootGameObject(GameObject g)
        {
            Transform temp = g.transform;
            while(temp.parent != null)
            {
                temp = temp.parent;
            }
            return temp.gameObject;
        }
    }
}

