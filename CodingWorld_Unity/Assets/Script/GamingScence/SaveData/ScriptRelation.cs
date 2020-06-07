using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRelation : MonoBehaviour
{
    public Hashtable scriptRelation = new Hashtable();
    public string worldname;


    private void Start()
    {
        if (gameObject.name.Equals("Player"))
        {
            scriptRelation = GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().scriptRelation;
        }
    }

    public void registerRelation(string gameObject, string script)
    {
        scriptRelation.Add(gameObject,script);
        Debug.Log("add success");
    }

    public Hashtable getRelation()
    {
        return scriptRelation;
    }

}
