using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class showOnfield : MonoBehaviour
{
    public InputField iptf;
    public TextAsset txA;
    private void Awake()
    {
        show();
    }

    public void show()
    {
        //StreamReader sr = new StreamReader("Assets\\Script\\addscript\\prototype.txt");
        //string contain = sr.ReadToEnd();
        iptf.text = txA.text;

    }
}
