using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRelation : MonoBehaviour
{
    public Hashtable scriptRelation = new Hashtable();
    public Dictionary<string, Dictionary<string, object>> animals =new Dictionary<string, Dictionary<string, object>>();

    public void addAnimal(string name, Dictionary<string, object> body)
    {
        animals.Add(name, body);
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
