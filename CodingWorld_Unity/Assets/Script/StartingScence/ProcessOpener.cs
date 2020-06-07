using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ProcessOpener : MonoBehaviour
{
    string path = "";


    void Awake()
    {
#if UNITY_EDITOR
        path = @"F:\\BaiduNetdiskDownload\\Majiang\\example\\CodingWorld_Data\\Managed\\";
#else
        path = Application.dataPath+"/Managed/";
#endif
        Process pr = new Process();
        pr.StartInfo.WorkingDirectory = path;
        pr.StartInfo.FileName = path + "ConsoleApp2.exe";

        pr.Start();
    }

    
}
