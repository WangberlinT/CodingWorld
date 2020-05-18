using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
    private World world;
    private Text text;

    private float frameRate = 0;
    private float timer = 0;
    private Dictionary<string,string> messages = new Dictionary<string, string>();

    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = MessageUpdate();

        if(timer > 1f)
        {
            frameRate = (int)(1f / Time.unscaledDeltaTime);
            timer = 0;
        }
        else
        {
            timer += Time.unscaledDeltaTime;
        }
    }

    private string MessageUpdate()
    {
        //Default message
        Transform playerTrans = world.player.transform;
        string debugText = "[Debug] Init debug screen successfully!\n";
        debugText += frameRate + "fps";
        debugText += "\n";
        debugText += "XYZ: (" + Mathf.FloorToInt(playerTrans.position.x)
                  + "," + Mathf.FloorToInt(playerTrans.position.y)
                  + "," + Mathf.FloorToInt(playerTrans.position.z);
        debugText += "\n";
        debugText += "ActiveChunk num: " + world.getActiveChunkListSize();
        debugText += "\n";

        debugText += "ChunkCreating: " + world.getChunkCreating();
        debugText += "\n";
        //------------------
        //Pass message
        foreach (KeyValuePair<string, string> kv in messages)
        {
            debugText += "[" + kv.Key + "]";
            debugText += "    ";
            debugText += kv.Value;
            debugText += "\n";
        }
        return debugText;
    }

    public void LogMessage(string tag,string message)
    {
        messages[tag] = message;
    }
}
