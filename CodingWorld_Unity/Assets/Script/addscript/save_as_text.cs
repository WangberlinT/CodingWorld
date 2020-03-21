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
        string pet_name = GameObject.Find("PetnameInputField").GetComponent<InputField>().text;
        save_value=save_value.Replace("moban", pet_name);
        string[] in_str = save_value.Split(new char[] { '\r','\n'},System.StringSplitOptions.RemoveEmptyEntries);
        File.WriteAllLines(path+pet_name+".cs",in_str );
        AssetDatabase.Refresh();
        GameObject.Find("WriteCanvas").SetActive(false);

    }
}
