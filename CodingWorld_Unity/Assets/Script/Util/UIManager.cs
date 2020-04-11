using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject StartMenu;
    public InputField userInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exist, destory object");
            Destroy(this);
        }
    }

    public void ConnectedToServer()
    {
        //StartMenu.SetActive(false);
        //userInput.interactable = false;
        Client.instance.ConnectedToServer();
    }

    public void Send()
    {
        ClientSend.SendCode();
    }
}
