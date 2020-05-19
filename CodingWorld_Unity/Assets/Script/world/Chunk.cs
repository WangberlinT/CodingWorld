using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk 
{
    public ChunkCoord coord;

    private MeshRenderer renderer;
    private MeshFilter filter;
    private GameObject chunkObject;

    private MeshCollider meshCollider;

    private int vertexIndex = 0;
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    //voxelMap 记录 Chunk 中 block 的类型参数
    private int[,,] voxelMap = new int[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];
    private World world;
    private bool _isActive;
    public bool isVoxelMapPopulated = false;
    public Chunk(ChunkCoord _coord, World _world, bool generateOnload)
    {
        world = _world;
        coord = _coord;
        _isActive = true;
        if(generateOnload)
        {
            Init();
        }

    }

    public void Init()
    {
        chunkObject = new GameObject();
        filter = chunkObject.AddComponent<MeshFilter>();
        renderer = chunkObject.AddComponent<MeshRenderer>();

        meshCollider = chunkObject.AddComponent<MeshCollider>();
        //设置chunk 属性
        renderer.material = world.material;
        chunkObject.transform.SetParent(world.transform);
        chunkObject.transform.position = new Vector3(coord.x * VoxelData.ChunkWidth, 0, coord.z * VoxelData.ChunkWidth);
        chunkObject.name = "Chunk " + coord.x + ", " + coord.z;
        //初始化chunk 中block类型
        PopulateVoxelMap();
        //添加Chunk露出的面
        UpdateChunk();
        

        meshCollider.sharedMesh = filter.mesh;
    }

    void UpdateChunk()
    {
        ClearMeshData();

        for (int y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    if(world.blocktypes[voxelMap[x,y,z]].isSolid)
                        UpdateMeshData(new Vector3(x, y, z));
                }
            }
        }

        CreateMesh();
    }

    void ClearMeshData()
    {
        vertexIndex = 0;
        vertices.Clear();
        triangles.Clear();
        uvs.Clear(); 
    }

    void PopulateVoxelMap()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    //从world中获取Voxel 类型
                    voxelMap[x,y,z] = world.GetVoxel(new Vector3(x, y, z) + position);
                }
            }
        }

        isVoxelMapPopulated = true;
    }

    public bool isActive
    {
        get { return _isActive; }
        set 
        {
            _isActive = value;
            if (chunkObject != null)
                chunkObject.SetActive(value);
        }
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

    private Vector3 GetVoxelPosInChunk(Vector3 pos)
    {
        int xCheck = Mathf.FloorToInt(pos.x);
        int yCheck = Mathf.FloorToInt(pos.y);
        int zCheck = Mathf.FloorToInt(pos.z);

        xCheck -= Mathf.FloorToInt(chunkObject.transform.position.x);
        zCheck -= Mathf.FloorToInt(chunkObject.transform.position.z);
        return new Vector3(xCheck, yCheck, zCheck);
    }

    public void EditVoxel(Vector3 pos, int newID)
    {
        Vector3 checkPos = GetVoxelPosInChunk(pos);
        voxelMap[(int)checkPos.x, (int)checkPos.y, (int)checkPos.z] = newID;

        UpdateSurroundingChunkVoxel((int)checkPos.x, (int)checkPos.y, (int)checkPos.z);
        UpdateChunk();
    }

    void UpdateSurroundingChunkVoxel(int x, int y, int z)
    {
        Vector3 thisVoxel = new Vector3(x, y, z);

        for(int p = 0; p < 6; p ++)
        {
            Vector3 currentVoxel = thisVoxel + VoxelData.faceChecks[p];

            if(!IsVoxelInChunk((int)currentVoxel.x, (int)currentVoxel.y, (int)currentVoxel.z))
            {
                world.GetChunkFromVector3(currentVoxel + position).UpdateChunk();
            }
        }
    }

    public int GetVoxelFromGlobalVector3 (Vector3 pos)
    {
        Vector3 checkPos = GetVoxelPosInChunk(pos);

        return voxelMap[(int)checkPos.x, (int)checkPos.y, (int)checkPos.z];
    }

    void UpdateMeshData(Vector3 pos)
    {
        //6个面
        for (int p = 0; p < 6; p++)
        {
            //如果不在Chunk内部，绘制面信息
            if(!CheckVoxel(pos+ VoxelData.faceChecks[p]))
            {
                //block 类型
                int blockID = voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];
                //设置每个面的参数，每个面由两个三角形构成，共6个顶点TODO:优化成4个顶点
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

    //将先前设置的Mesh信息绑定到mesh filter上
    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        
        mesh.RecalculateNormals();
        filter.mesh = mesh;
        meshCollider.sharedMesh = filter.mesh;
    }

    bool IsVoxelInChunk(int x, int y, int z)
    {
        if (x < 0 || x > VoxelData.ChunkWidth - 1 || y < 0 || y > VoxelData.ChunkHeight - 1 || z < 0 || z > VoxelData.ChunkWidth - 1)
            return false;
        else
            return true;
    }

    //根据textureID添加指定ID的贴图
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

    public ChunkCoord()
    {
        x = 0;
        z = 0;
    }
    public ChunkCoord(int _x, int _z)
    {
        x = _x;
        z = _z;
    }

    public ChunkCoord(Vector3 pos)
    {
        int xCheck = Mathf.FloorToInt(pos.x);
        int zCheck = Mathf.FloorToInt(pos.z);

        x = xCheck / VoxelData.ChunkWidth;
        z = zCheck / VoxelData.ChunkWidth;
    }

    public override bool Equals(object o)
    {
        if(o.GetType() == typeof(ChunkCoord))
        {
            ChunkCoord other = (ChunkCoord)o;
            if (other == null)
                return false;
            else if (other.x == x && other.z == z)
                return true;
            else
                return false;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() + z.GetHashCode();
    }



}

