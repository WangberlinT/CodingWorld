using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button addbtn;
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
        GameObject.Find("AddCanvas").SetActive(false);

    }
}
