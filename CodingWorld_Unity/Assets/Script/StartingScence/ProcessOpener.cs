using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProcessOpener : MonoBehaviour
{
    string exepath = @"F:\\BaiduNetdiskDownload\\Majiang\\CodingWorld\\CompilerApp\\ConsoleApp2\\ConsoleApp2\\bin\\Debug\\ConsoleApp2.exe";
    // Start is called before the first frame update
    void Awake()
    {
        Process.Start(exepath);
    }

    
}
