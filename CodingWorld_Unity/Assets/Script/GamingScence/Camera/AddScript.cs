using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AddScript : MonoBehaviour
{
    public GameObject add;
    public GameObject wrt;
    public InputField pet;
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
            Transform camTrans = transform.Find("Main Camera");
            Ray ray = new Ray(camTrans.position, camTrans.forward);
            RaycastHit hitt = new RaycastHit();
            if(Physics.Raycast(ray, out hitt, 5f))
            {
                GameObject seeObject= hitt.collider.gameObject;
                string name = seeObject.name;
                Debug.Log(seeObject.name);
                string qualifiedName = typeof(ObjectManager).AssemblyQualifiedName;
                if (seeObject.TryGetComponent(Type.GetType(qualifiedName),out Component c))
                {
                    Debug.Log("see it");
                    Cursor.lockState = CursorLockMode.None;
                    add.gameObject.SetActive(true);
                    pet.text = name;
                    gameObject.GetComponent<ConflictControl>().notgamescene = false;
                }
            }
            
        }          
        
    }
    void Write()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.None;
            wrt.gameObject.SetActive(true);
            gameObject.GetComponent<ConflictControl>().notgamescene = false;
        }
    }
}
