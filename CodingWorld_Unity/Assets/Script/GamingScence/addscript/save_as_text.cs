using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using InGameCompiler;
using System.Diagnostics;
using System;

public class save_as_text : MonoBehaviour
{

    // Start is called before the first frame update
    public Button m_Button;

    void Start()
    {
        m_Button.onClick.AddListener(ButtonOnClickEvent);
    }

    public void ButtonOnClickEvent()
    {
        UnityEngine.Debug.Log("push Button");
        string code = GameObject.Find("mainbodyinputField").GetComponent<InputField>().text;
        string pet_name = GameObject.Find("PetnameInputField").GetComponent<InputField>().text;
        code=code.Replace("moban", pet_name);
        GameObject.Find("CodeEditor").GetComponent<UIManager>().Send();
        GameObject.Find("CodeEditor").SetActive(false);
        GameObject.Find("Main Camera").GetComponent<ConflictControl>().notgamescene = true;

    }
}
