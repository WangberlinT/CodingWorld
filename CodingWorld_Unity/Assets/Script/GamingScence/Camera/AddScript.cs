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
        if ((!add.activeSelf) && (!wrt.activeSelf))
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
            gameObject.GetComponent<ConflictControl>().notgamescene = false;
        }          
        
    }
    void Write()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            wrt.gameObject.SetActive(true);
            gameObject.GetComponent<ConflictControl>().notgamescene = false;
        }
    }
}
