using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class AddScript : MonoBehaviour
{
    public GameObject add;
    public GameObject wrt;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if ((!add.active) && (!wrt.active))
        {
        Add();
        Write();
        
        }
        
    }

    void Add()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            add.gameObject.SetActive(true);
        }          
        
    }
    void Write()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            wrt.gameObject.SetActive(true);
        }
    }
}
