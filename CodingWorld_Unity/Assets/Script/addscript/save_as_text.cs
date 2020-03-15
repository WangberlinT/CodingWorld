using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


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
        string path="Assets\\TXT\\";
        Debug.Log("push Button");
        string save_value = GameObject.Find("mainbodyinputField").GetComponent<InputField>().text;
        Debug.Log(save_value);
        string[] in_str = save_value.Split('\n');
        string text_value = GameObject.Find("InputField").GetComponent<InputField>().text;
        File.WriteAllLines(path+text_value,in_str );
        AssetDatabase.Refresh();
        GameObject.Find("Canvas").SetActive(false);

    }
}
