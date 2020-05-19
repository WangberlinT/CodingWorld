
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;

public class ButtonActivity : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public GameObject go;

    public void StartGameButton()
    {
        GameObject buttonpair= GameObject.Find("Panel").transform.Find("LoadCanvas").gameObject;
        buttonpair.SetActive(true);
        GameObject.Find("BasicButton").SetActive(false);
    }

    public void StartNewGameButton()
    {
        SceneManager.LoadScene("Gaming");

    }

    public void QuitButton()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void QuitWithSave()
    {
        saveData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void BackButton()
    {
        
        go.SetActive(false);
        GameObject.Find("Player").GetComponent<ConflictControl>().notgamescene = true;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void loadSave()
    {
        string fp = "./relation.json";
        Hashtable relation = JsonUtility.FromJson<Hashtable>(File.ReadAllText(fp));
        foreach (string key in relation.Keys)
        {
            Debug.Log(key);
        }
        
    }

    public void saveData()
    {
        Hashtable relation = GameObject.Find("Player").GetComponent<ScriptRelation>().scriptRelation;
        string fp = ".\\relation.json";
        FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
        fs1.Close();
        Debug.Log(HashtableToWxJson(relation));
        File.WriteAllText(fp,HashtableToWxJson(relation) );
        

    }

    public string HashtableToWxJson(Hashtable data)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (object key in data.Keys)
            {
                object value = data[key];
                sb.Append("\"");
                sb.Append(key);
                sb.Append("\":\"");
                if (!String.IsNullOrEmpty(value.ToString()) && value != DBNull.Value)
                {
                    sb.Append(value).Replace("\\", "/");
                }
                else
                {
                    sb.Append(" ");
                }
                sb.Append("\",");
            }
            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }
        catch (Exception ex)
        {

            return "";
        }
    }

}
