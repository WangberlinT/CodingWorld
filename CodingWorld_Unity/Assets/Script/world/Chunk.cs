using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk 
{
    public ChunkCoord coord;

    private MeshRenderer renderer;
    private MeshFilter filter;
    private GameObject chunkObject;

    private int vertexIndex = 0;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    //voxelMap 记录 Chunk 中 block 的类型参数
    private byte[,,] voxelMap = new byte[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];
    private World world;
    public Chunk(ChunkCoord _coord, World _world)
    {
        world = _world;
        coord = _coord;
        chunkObject = new GameObject();
        filter = chunkObject.AddComponent<MeshFilter>();
        renderer = chunkObject.AddComponent<MeshRenderer>();
        //设置chunk 属性
        renderer.material = world.material;
        chunkObject.transform.SetParent(world.transform);
        chunkObject.transform.position = new Vector3(coord.x * VoxelData.ChunkWidth,0 ,coord.z * VoxelData.ChunkWidth);
        chunkObject.name = "Chunk " + coord.x + ", " + coord.z;
        //初始化chunk 中block类型
        PopulateVoxelMap();
        //添加Chunk露出的面
        CreateChunkMesh();
        CreateMesh();
    }

    void CreateChunkMesh()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    AddVoxelDataToChunk(new Vector3(x, y, z));
                }
            }
        }
    }

    void PopulateVoxelMap()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    voxelMap[x,y,z] = world.GetVoxel(new Vector3(x, y, z) + position);
                }
            }
        }
    }

    public bool isActive
    {
        get { return chunkObject.activeSelf; }
        set { chunkObject.SetActive(value); }
    }

    public Vector3 position
    {
        get { return chunkObject.transform.position; }
    }

    bool CheckVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        if (!IsVoxelInChunk(x, y, z))
            return false;
        //TODO: chunk 优化
        //world.blocktypes[world.GetVoxel(pos + position)].isSolid;
        return world.blocktypes[voxelMap[x, y, z]].isSolid;
    }

    void AddVoxelDataToChunk(Vector3 pos)
    {
        for (int p = 0; p < 6; p++)
        {
            if(!CheckVoxel(pos+ VoxelData.faceChecks[p]))
            {
                //block 类型
                byte blockID = voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];
                //设置6个面的参数
                for (int i = 0; i < 6; i++)
                {
                    //三角形参数
                    int triangleIndex = VoxelData.voxelTris[p, i];
                    //加上block 的position偏移得到世界坐标系中的位置
                    vertices.Add(VoxelData.voxelVertex[triangleIndex] + pos);
                    triangles.Add(vertexIndex);
                    vertexIndex++;
                }
                //加贴图
                AddTexture(world.blocktypes[blockID].GetTextureID(p));
            }
        }
    }

    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        
        mesh.RecalculateNormals();
        filter.mesh = mesh;
    }

    bool IsVoxelInChunk(int x, int y, int z)
    {
        if (x < 0 || x > VoxelData.ChunkWidth - 1 || y < 0 || y > VoxelData.ChunkHeight - 1 || z < 0 || z > VoxelData.ChunkWidth - 1)
            return false;
        else
            return true;
    }

    void AddTexture(int textureID)
    {
        float y = (textureID / VoxelData.TextureAtlasSizeInBlocks)*VoxelData.NormalizedBlockTextureSize;
        float x = (textureID % VoxelData.TextureAtlasSizeInBlocks)*VoxelData.NormalizedBlockTextureSize;

        for(int i = 0;i < 6;i ++)
        {
            uvs.Add(new Vector2(x, y) + VoxelData.voxelUvs[i] * VoxelData.NormalizedBlockTextureSize);
        }
    }
}

public class ChunkCoord
{
    public int x;
    public int z;
    public ChunkCoord(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public bool Equals(ChunkCoord other)
    {
        if (other == null)
            return false;
        else if (other.x == x && other.z == z)
            return true;
        else
            return false;
    }
}

