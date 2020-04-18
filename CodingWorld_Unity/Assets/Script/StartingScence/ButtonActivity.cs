using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public void BackButton()
    {
        go.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<ConflictControl>().notgamescene = true;
    }

}
