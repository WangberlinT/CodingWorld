using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {    
    }
    //
    void Update()
    {             if (Input.GetKeyUp(KeyCode.A))
        {
            gameObject.SetActive(false);
        }
    }
}

