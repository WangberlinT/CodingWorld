using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/**
 *  每局游戏开始时，记录玩家的行为：
 *  1. 方块的变化 
 *      Block name:str
 *      Block index: int
 *      Block pos: [int,int,int] unique
 *      Create: true/false
 *  2. 玩家信息
 *      position: [float,float,float]
 *  3. world 信息(TODO)
 *      seed
 *      
 *  Save as Json
 *  
 *  Load data
 *      Load to world
 */

public class GameRecorder
{
    private Dictionary<Vector3, CubeData> cubes;
    private PlayerData playerData;
    private static GameRecorder instance;

    private GameRecorder()
    {
        cubes = new Dictionary<Vector3, CubeData>();
       
    }

    public static GameRecorder GetInstance()
    {
        if (instance == null)
            instance = new GameRecorder();
        return instance;
    }

    public bool HasRecord()
    {
        return cubes != null && playerData != null;
    }

    public void UpdateCubes(CubeData c)
    {
        Debug.Log("Update Cube" + c.GetPosition());
        cubes[c.GetPosition()] = c;
    }

    public void UpdatePlayer(Vector3 pos)
    {
        playerData = new PlayerData(pos);
    }

    public void Load(string jsonFile)
    {
        WorldData temp = JsonUtility.FromJson<WorldData>(jsonFile);
        foreach(CubeData c in temp.cubeDatas)
        {
            cubes[c.GetPosition()] = c;
        }
        this.playerData = temp.playerData;
    }

    public string SaveAsJson()
    {
        WorldData save = new WorldData(playerData, GetCubeDatas());
        return JsonUtility.ToJson(save);
    }

    public PlayerData GetPlayerData()
    {
        return this.playerData;
    }

    public List<CubeData> GetCubeDatas()
    {
        List<CubeData> datas = new List<CubeData>();
        datas.AddRange(cubes.Values);
        return datas;
    }

}
[System.Serializable]
public class WorldData  
{
    public PlayerData playerData;
    public List<CubeData> cubeDatas;

    public WorldData(PlayerData playerData, List<CubeData> cubeDatas)
    {
        this.playerData = playerData;
        this.cubeDatas = cubeDatas;
    }
}

[System.Serializable]
public class PlayerData
{
    public float[] position = new float[3];

    public PlayerData(Vector3 pos)
    {
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
    }

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
}

[System.Serializable]
public class CubeData
{
    public string name;
    public int index;
    public bool creation;
    public int[] position;


    public CubeData(string name, int index, bool creation, Vector3 pos)
    {
        int[] position = new int[3];
        position[0] = Mathf.FloorToInt(pos.x);
        position[1] = Mathf.FloorToInt(pos.y);
        position[2] = Mathf.FloorToInt(pos.z);

        this.position = position;
        this.name = name;
        this.index = index;
        this.creation = creation;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }

    public int GetIndex()
    {
        if (creation)
            return index;
        else
            return 0;//air
    }

    
}
