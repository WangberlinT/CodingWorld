using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petmove : MonoBehaviour
{
    // Start is called before the first frame update
    Movable mo;
    GameObject master;
    PetState state;
    bool first=false;
    void Awake()
    {
        mo = gameObject.GetComponent<Movable>();
        master = GameObject.Find("master");
        state = PetState.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (!first)
        {
            mo.MoveTo(mo.Forward(4), 2f);
            //mo.jumpForward();
            first = true;
        }
     
    }

   
}

enum PetState {Empty,Follow,Loop }
