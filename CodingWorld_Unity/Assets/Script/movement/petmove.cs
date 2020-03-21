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
        changemode();
        
        if (state == PetState.Loop&&first)
        {
            Vector3[] p = { new Vector3(0,0.5f,0),new Vector3(1,0.5f,1),new Vector3(2,0.5f,3),new Vector3(-1,0.5f,-1),gameObject.transform.position };
            mo.MovePath(p,5) ;
            first = false;
        }
        if (state == PetState.Follow)
        {
             if ((gameObject.transform.position - master.transform.position).sqrMagnitude > 9.5f)
                    {
                         mo.follow("master");
                    }
        }

     
    }

    public void changemode()
    {
        
        if (Input.GetKeyUp(KeyCode.F))
        {
            iTween.Stop();
            state = PetState.Follow;
            first = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            iTween.Stop();
            state = PetState.Empty;
            first = true;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            iTween.Stop();
            state = PetState.Loop;
            first = true;
        }
    }
}

enum PetState {Empty,Follow,Loop }
