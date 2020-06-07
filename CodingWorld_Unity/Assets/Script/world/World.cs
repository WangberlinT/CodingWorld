using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int seed;

    public Transform player;
    public Vector3 spawnPosition;
    public Material material;
    public BlockType[] blocktypes;

    public GameObject debugScreen;

    private Chunk[,] chunks = new Chunk[VoxelData.WorldSizeInChunks, VoxelData.WorldSizeInChunks];
    private List<ChunkCoord> activeChunks = new List<ChunkCoord>();
    private ChunkCoord playerLastChunkCoord;
    private ChunkCoord playerChunkCoord;
    private List<ChunkCoord> chunkToCreate = new List<ChunkCoord>();
    private bool chunkCreating;
    private CreatureManager creatureManager = new CreatureManager();
    

    private void Start()
    {
        Random.InitState(seed);
        debugScreen.SetActive(false);
        spawnPosition = new Vector3((VoxelData.WorldSizeInChunks * VoxelData.ChunkWidth) / 2f, VoxelData.ChunkHeight, (VoxelData.WorldSizeInChunks * VoxelData.ChunkWidth) / 2f);
        LoadWorld();
        GenerateWorld();
        creatureManager.GenerateCreatures();
        
        playerLastChunkCoord = GetChunkCoordFromVector3(player.position);
        
    }

    private void LoadWorld()
    {
        GameRecorder recorder = GameRecorder.GetInstance();
        if (recorder.HasRecord())
        {
            spawnPosition = recorder.GetPlayerData().GetPlayerPosition();
            //add chunks and update cubes
            List<CubeData> cubeDatas = recorder.GetCubeDatas();

            foreach (CubeData cubeData in cubeDatas)
            {
                Vector3 tempPos = cubeData.GetPosition();
                int x = Mathf.FloorToInt(tempPos.x / VoxelData.ChunkWidth);
                int z = Mathf.FloorToInt(tempPos.z / VoxelData.ChunkWidth);
                if (chunks[x, z] == null)
                {
                    //TODO: new Chunks,not init
                    chunks[x, z] = new Chunk(new ChunkCoord(x, z), this, true);
                }

                //TODO: update voxel
                chunks[x, z].EditVoxel(tempPos, cubeData.GetIndex());
            }

            //TODO: load creatures
            creatureManager.LoadCreatureDatas(recorder.GetCreatureDatas());
        }
    }

    private void Update()
    {
        playerChunkCoord = GetChunkCoordFromVector3(player.position);
        if (!playerLastChunkCoord.Equals(playerChunkCoord))
            CheckViewDistance();

        if(chunkToCreate.Count > 0 && !chunkCreating)
        {
            Debug.Log("ChunkTOCreate Number = " + chunkToCreate.Count);
            StartCoroutine(CreateChunks());
        }

        if (Input.GetKeyDown(KeyCode.F3))
            debugScreen.SetActive(!debugScreen.activeSelf);

        GameRecorder.GetInstance().UpdatePlayer(player.position);
        playerLastChunkCoord = playerChunkCoord;
    }

    public void AddCreature(Vector3 pos)
    {
        creatureManager.AddCreature(new CreatureData(pos));
        GameRecorder.GetInstance().SetCreatureDatas(creatureManager.GetCreatureDatas());
        
    }

    public void DeleteCreature(Vector3 pos)
    {
        Debug.Log("Delete " + pos);
        creatureManager.DeleteCreature(pos);
        GameRecorder.GetInstance().SetCreatureDatas(creatureManager.GetCreatureDatas());
    }

    public int getActiveChunkListSize()
    {
        return activeChunks.Count;
    }

    public bool getChunkCreating() { return chunkCreating; }

    //初始化生成第一个区域块，只被调用一次
    void GenerateWorld()
    {
        for(int x = (VoxelData.WorldSizeInChunks/2) - VoxelData.ViewDistanceInChunks; x < (VoxelData.WorldSizeInChunks / 2) + VoxelData.ViewDistanceInChunks; x ++)
        {
            for(int z = (VoxelData.WorldSizeInChunks / 2) - VoxelData.ViewDistanceInChunks; z < (VoxelData.WorldSizeInChunks / 2) + VoxelData.ViewDistanceInChunks; z ++)
            {
                if(chunks[x,z] == null)
                {
                    chunks[x, z] = new Chunk(new ChunkCoord(x, z), this, true);
                }
                
                activeChunks.Add(new ChunkCoord(x, z));
                
            }
        }
        //将玩家位置设置为出生位置
        player.position = spawnPosition;
    }

    IEnumerator CreateChunks()
    {
        chunkCreating = true;
        while(chunkToCreate.Count > 0)
        {
            chunks[chunkToCreate[0].x, chunkToCreate[0].z].Init();
            chunkToCreate.RemoveAt(0);
            yield return null;
        }
        chunkCreating = false;
    }

    ChunkCoord GetChunkCoordFromVector3 (Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / VoxelData.ChunkWidth);
        int z = Mathf.FloorToInt(pos.z / VoxelData.ChunkWidth);
        return new ChunkCoord(x, z);
    }

    public Chunk GetChunkFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / VoxelData.ChunkWidth);
        int z = Mathf.FloorToInt(pos.z / VoxelData.ChunkWidth);
        return chunks[x, z];
    }

    void CheckViewDistance()
    {
        playerLastChunkCoord = playerChunkCoord;
        ChunkCoord coord = GetChunkCoordFromVector3(player.position);
        List<ChunkCoord> inActiveChunk = new List<ChunkCoord>(activeChunks);
        for(int x = coord.x - VoxelData.ViewDistanceInChunks; x < coord.x + VoxelData.ViewDistanceInChunks;x++)
        {
            for (int z = coord.z - VoxelData.ViewDistanceInChunks; z < coord.z + VoxelData.ViewDistanceInChunks; z++)
            {
                if(IsChunkInWorld(new ChunkCoord(x,z)))
                {
                    if (chunks[x, z] == null)
                    {
                        chunks[x, z] = new Chunk(new ChunkCoord(x, z), this, false);
                        chunkToCreate.Add(new ChunkCoord(x, z));
                    }
                    else if(!chunks[x,z].isActive)
                    {
                        chunks[x, z].isActive = true;
                        activeChunks.Add(new ChunkCoord(x, z));
                    }
                    //将仍需显示的从列表中删除
                    for (int i = 0; i < inActiveChunk.Count; i++)
                    {
                       if(inActiveChunk[i].Equals(new ChunkCoord(x, z)))
                          inActiveChunk.RemoveAt(i);
                     
                    }
                }
            }
        }

        Debug.Log("ActiveChunks Num = " + activeChunks.Count);
        //剩余的都是不需要显示的，全部设置为false
        foreach (ChunkCoord c in inActiveChunk)
        {
            chunks[c.x, c.z].isActive = false;
            activeChunks.Remove(c);
        }
            
    }

    public bool CheckForVoxel(Vector3 pos)
    {
        ChunkCoord thisChunk = new ChunkCoord(pos);

        if (IsVoxelInWorld(pos))
            return false;

        if (chunks[thisChunk.x, thisChunk.z] != null && chunks[thisChunk.x, thisChunk.z].isVoxelMapPopulated)
            return blocktypes[chunks[thisChunk.x, thisChunk.z].GetVoxelFromGlobalVector3(pos)].isSolid;

        return blocktypes[GetVoxel(pos)].isSolid;
    }

    public byte GetVoxel(Vector3 pos)
    {
        int ypos = Mathf.FloorToInt(pos.y);

        if (!IsVoxelInWorld(pos))
            return 0;//air
        if (ypos == 0)
            return 1;//stone

        /* BASIC TERRAIN PASS */

        int terrainHeight = Mathf.FloorToInt(Noise.Get2DPerlin(new Vector2(pos.x, pos.z),100, 0.2f)*VoxelData.ChunkHeight);
        if (ypos < terrainHeight)
            return 1;//stone
        else if (ypos == terrainHeight)
            return 2;//dirt
        else
            return 0;//air
    }

    bool IsChunkInWorld(ChunkCoord coord)
    {
        if (coord.x > 0 && coord.x < VoxelData.WorldSizeInChunks - 1 && coord.z > 0 && coord.z < VoxelData.WorldSizeInChunks - 1)
            return true;
        else
            return false;
    }

    bool IsVoxelInWorld(Vector3 pos)
    {
        if (pos.x >= 0 && pos.x < VoxelData.WorldSizeInVoxels && pos.y >= 0 && pos.y < VoxelData.WorldSizeInVoxels && pos.z >= 0 && pos.z < VoxelData.WorldSizeInVoxels)
            return true;
        else
            return false;
    }
}

[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int buttonFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;

    public int GetTextureID(int faceIndex)
    {
        //TODO: use enum
        switch(faceIndex)
        {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return buttonFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;

            default:
                Debug.Log("Error in GetTextureID");
                return -1;
        }
    }
}

[System.Serializable]
public class Setting
{
    //视野为八个chunk 
    public int viewDistance = 8;
    //加载距离为16个chunk
    public int loadDistance = 16;

}

