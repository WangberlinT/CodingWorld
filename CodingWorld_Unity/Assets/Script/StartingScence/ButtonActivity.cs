﻿
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
using System.Text.RegularExpressions;

public class ButtonActivity : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public GameObject go;
    public InputField worldname;

    public void StartGameButton()
    {
        GameObject buttonpair= GameObject.Find("Panel").transform.Find("LoadCanvas").gameObject;
        buttonpair.SetActive(true);
        GameObject.Find("BasicButton").SetActive(false);
    }

    public void StartNewGameButton()
    {
        GameObject buttonpair = GameObject.Find("Panel").transform.Find("InputWordName").gameObject;
        buttonpair.SetActive(true);
        GameObject.Find("LoadCanvas").SetActive(false);
        

    }

    public void CreateNewWorldButton()
    {
        GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().worldname = worldname.text;
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

    /**
     * 保存加载好的json格式dict，物体：脚本Key-value pair
     */
    public void loadSave()
    {
        string fp = ".\\relation.json";
#if UNITY_EDITOR
        fp = ".\\save\\";
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
        GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().worldname = savefile.Replace(".json", "");
        savefile = fp + savefile;
        StreamReader streamReader = new StreamReader(savefile);
        string str = streamReader.ReadToEnd();
        if (str.Contains("animal"))
        {
            Debug.Log(str);
            string[] data = Regex.Split(str, ",\"animal\"");
            str = data[1];
            str = "{\"animal\"" + str;
            Debug.Log(str);
            //Hashtable json = JsonMapper.ToObject<Hashtable>(str);
            Dictionary<string, List<Dictionary<string, object>>> json = JsonMapper.ToObject<Dictionary<string, List<Dictionary<string, object>>>>(str);
            foreach (var animal in json["animal"])
            {

                if ((bool)animal["runstate"])
                {
                    GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().registerRelation((string)animal["name"], (string)animal["script"]);
                }

            }
            GameRecorder.GetInstance().Load( data[0] + '}');
            Debug.Log(data[0] + '}');
        }
        else
        {
            GameRecorder.GetInstance().Load( str);
        }
        SceneManager.LoadScene("Gaming");

        //TODO： load world data
    }

    

    public void saveData()
    {
        SaveWorldData();
        Hashtable relation = GameObject.Find("Player").GetComponent<ScriptRelation>().scriptRelation;
        string world_data = GameRecorder.GetInstance().SaveAsJson();
        string animal_data = "";
        if (relation.Keys.Count != 0) 
        {
            SaveData save_data = new SaveData();
            foreach (DictionaryEntry key in relation)
            {
                Debug.Log(key.Key);
                save_data.add(key.Key as string, key.Value as string);
            }
            Hashtable saveData = new Hashtable();
            saveData.Add("animal", save_data.object_data);
            animal_data=JsonMapper.ToJson(saveData);
        }
        string fp = ".\\relation.json";
#if UNITY_EDITOR
        fp = ".\\save\\";
#else
        fp = ".\\save\\";
#endif
        if (!Directory.Exists(fp)) Directory.CreateDirectory(fp);
        fp += GameObject.Find("DataTransfer").GetComponent<ScriptRelation>().worldname+".json";
        FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
        fs1.Close();
        world_data = world_data + animal_data;
        if(world_data.Contains("animal"))
            world_data = world_data.Replace("}{\"animal\"",",\"animal\"");
        Debug.Log(world_data);
        File.WriteAllText(fp,world_data );


        //TODO: save world Json
        
    }

    private void SaveWorldData()
    {
        string dir = ".\\save\\";
        string name = "test.json";

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        File.WriteAllText(dir + name, GameRecorder.GetInstance().SaveAsJson());
        Debug.Log("save at " + dir + name);
    }

   

}
