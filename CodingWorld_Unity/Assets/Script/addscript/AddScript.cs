using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AddScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Add();
        Drop();
        if (Input.GetKeyUp(KeyCode.W))
        {
            gameObject.transform.Find("Canvas").gameObject.SetActive(true);
        }
    }

    void Add()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
           
            string path = "Assets\\TXT\\pick.txt";
            StreamReader sr = new StreamReader(path);
            string[] str = sr.ReadToEnd().Split('\n');

            File.WriteAllLines("Assets\\Script\\addscript\\Disappear.cs", str);
            AssetDatabase.Refresh();
            gameObject.AddComponent<Disappear>();
        }
    }
    void Drop()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            Destroy(GetComponent<Disappear>());
        }
    }
}
