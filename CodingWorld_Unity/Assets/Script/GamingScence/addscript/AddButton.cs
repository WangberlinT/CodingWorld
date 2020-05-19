using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using System.IO;

using System;

using System.Reflection;
using UnityEngine.UI;


public class AddButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button addbtn;
    FileStream fs;

    Type type;
    void Start()
    {
        addbtn.onClick.AddListener(ButtonOnClickEvent);
        
    }

    public void ButtonOnClickEvent()
    {
        string pet = GameObject.Find("PetInputField").GetComponent<InputField>().text;
        Debug.Log(pet);
        string script = GameObject.Find("ScriptInputField").GetComponent<InputField>().text;
        ObjectManager obj = GameObject.Find(pet).GetComponent<ObjectManager>();
        obj.addAnimalScript(script);
        GameObject.Find("Player").GetComponent<ScriptRelation>().registerRelation(pet,script);
        GameObject petgo = GameObject.Find(pet);
        GameObject.Find("AddCanvas").SetActive(false);
        GameObject.Find("Player").GetComponent<ConflictControl>().notgamescene = true;

    }
    public Type GetType(string script)
    {

        fs = new FileStream(script+".dll", FileMode.OpenOrCreate);

        byte[] b = new byte[fs.Length];

        fs.Read(b, 0, b.Length);

        fs.Dispose();

        fs.Close();

        Assembly assembly = System.Reflection.Assembly.Load(b);

        type = assembly.GetType(script);

        return type;

    }
    /*Assembly asm = Assembly.GetExecutingAssembly();
    Animal a = (Animal)asm.CreateInstance(classname);
    Type t = a.GetType();

    a.SetBasicSight(eye.GetComponent<BasicSight>());
        a.SetMovable(gameObject.GetComponent<Movable>());

        user = a;
        user.Begin();*/

}
