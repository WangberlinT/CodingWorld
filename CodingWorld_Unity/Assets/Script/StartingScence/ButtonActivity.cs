
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using LitJson;
using System.Runtime.InteropServices;

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
        string fp = ".\\relation.json";
#if UNITY_EDITOR
        fp = ".\\";
#else
        fp = ".\\save\\";
#endif

        OpenFileName openFileName = new OpenFileName();
        openFileName.structSize = Marshal.SizeOf(openFileName);
        openFileName.filter = "json文件(*.json)\0*.json";
        openFileName.file = new string(new char[256]);
        openFileName.maxFile = openFileName.file.Length;
        openFileName.fileTitle = new string(new char[64]);
        openFileName.maxFileTitle = openFileName.fileTitle.Length;
        openFileName.initialDir = fp;
        openFileName.title = "Choose Your Save";
        openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        string savefile = "";
        if (LocalDialog.GetOpenFileName(openFileName))
        {
            savefile = openFileName.fileTitle;
        }
        savefile = fp + savefile;
        StreamReader streamReader = new StreamReader(savefile);
        string str = streamReader.ReadToEnd();
        //Hashtable json = JsonMapper.ToObject<Hashtable>(str);
        SceneManager.LoadScene("Gaming");
        Dictionary<string, List<Dictionary<string, object>>> json = JsonMapper.ToObject<Dictionary<string, List<Dictionary<string, object>>>>(str);
        foreach (var animal in json["animal"])
        {
            
            if ((bool)animal["runstate"])
            {
                GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().registerRelation((string)animal["name"], (string)animal["script"]);
                GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().addAnimal((string)animal["name"],animal);
            }
                

        }
        Debug.Log(json["animal"][0]["script"]);
        

    }

    

    public void saveData()
    {
        Hashtable relation = GameObject.Find("Player").GetComponent<ScriptRelation>().scriptRelation;
        if (relation.Keys.Count == 0) return;
        SaveData save_data = new SaveData();
        foreach (DictionaryEntry key in relation)
        {
            Debug.Log(key.Key);
            save_data.add(key.Key as string,key.Value as string);
        }
        Hashtable saveData = new Hashtable();
        saveData.Add("animal", save_data.object_data);
        Debug.Log(JsonMapper.ToJson(saveData));
        string fp = ".\\relation.json";
#if UNITY_EDITOR
        fp = ".\\save\\";
#else
        fp = ".\\save\\";
#endif
        if (!Directory.Exists(fp)) Directory.CreateDirectory(fp);
        fp += "relation.json";
        FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
        fs1.Close();
        
        File.WriteAllText(fp, JsonMapper.ToJson(saveData));        
    }

   

}
