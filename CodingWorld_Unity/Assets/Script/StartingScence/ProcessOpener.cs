using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ProcessOpener : MonoBehaviour
{
    string exepath = @".\\CodingWorld_Data\\Managed\\ConsoleApp2.exe";
    // Start is called before the first frame update
    void Awake()
    {
        Process.Start(exepath);
    }

    
}
