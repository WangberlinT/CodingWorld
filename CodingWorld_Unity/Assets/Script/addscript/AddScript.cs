using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public class AddScript : MonoBehaviour
{
    public GameObject add;
    public GameObject wrt;
    public Component movement;
    public Component spin;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if ((!add.active) && (!wrt.active))
        {
        Add();
        Write();
        
        }
        
    }

    void Add()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            add.gameObject.SetActive(true);
            //测试用，稍后删除
            gameObject.GetComponent<Move>().enabled = false;
            gameObject.GetComponent<Spin>().enabled = false;
        }          
        
    }
    void Write()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            wrt.gameObject.SetActive(true);
            //测试用，稍后删除
            gameObject.GetComponent<Move>().enabled = false;
            gameObject.GetComponent<Spin>().enabled = false;
        }
    }
}
