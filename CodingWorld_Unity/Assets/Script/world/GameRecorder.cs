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
    private List<CreatureData> creatureDatas;
    private static GameRecorder instance;
    public static int creatureIndex = 0;

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

    public void SetCreatureDatas(List<CreatureData> datas)
    {
        creatureDatas = datas;
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
        this.creatureDatas = temp.creatureDatas;
        creatureIndex = temp.creatureIndex;
    }

    public List<CreatureData> GetCreatureDatas()
    {
        return creatureDatas;
    }

    public string SaveAsJson()
    {
        GameObject.Find("World").GetComponent<World>().SaveUpdate();
        WorldData save = new WorldData(playerData, GetCubeDatas(),creatureDatas,creatureIndex);
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
    public List<CreatureData> creatureDatas;
    public int creatureIndex;

    public WorldData(PlayerData playerData, List<CubeData> cubeDatas, List<CreatureData> creatureDatas, int index)
    {
        this.playerData = playerData;
        this.cubeDatas = cubeDatas;
        this.creatureDatas = creatureDatas;
        this.creatureIndex = index;
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
