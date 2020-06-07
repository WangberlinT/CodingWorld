using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameObject.GetComponent<ConflictControl>().notgamescene)
            {
                settingCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                gameObject.GetComponent<ConflictControl>().notgamescene = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                foreach (Transform child in GameObject.Find("Main Camera").transform)
                {
                    //if(!child.name.Equals("Main Camera"))
                    child.gameObject.SetActive(false);

                }
                gameObject.GetComponent<ConflictControl>().notgamescene = true;
            }
        }
    }
}
