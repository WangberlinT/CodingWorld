using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int ChunkWidth = 5;
    public static readonly int ChunkHeight = 5;

    public static readonly int TextureAtlasSizeInBlocks = 4;

    public static float NormalizedBlockTextureSize
    {
        get { return 1f / TextureAtlasSizeInBlocks; }
    }

    public static readonly Vector3[] voxelVertex = new Vector3[8]
    {
        //(x,y,z) Unity 里 y 正方向朝上
        //后面的4个
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(1.0f,0.0f,0.0f),
        new Vector3(1.0f,1.0f,0.0f),
        new Vector3(0.0f,1.0f,0.0f),
        //前面的4个
        new Vector3(0.0f,0.0f,1.0f),
        new Vector3(1.0f,0.0f,1.0f),
        new Vector3(1.0f,1.0f,1.0f),
        new Vector3(0.0f,1.0f,1.0f)
    };

    public static readonly Vector3[] faceChecks = new Vector3[6]
    {
        new Vector3(0.0f,0.0f,-1.0f),
        new Vector3(0.0f,0.0f,1.0f),
        new Vector3(0.0f,1.0f,0.0f),
        new Vector3(0.0f,-1.0f,0.0f),
        new Vector3(-1.0f,0.0f,0.0f),
        new Vector3(1.0f,0.0f,0.0f)
    };

    public static readonly int[,] voxelTris = new int[6, 6]
    {
        {0,3,1,1,3,2},//后
        {5,6,4,4,6,7},//前
        {3,7,2,2,7,6},//上
        {1,5,0,0,5,4},//下
        {4,7,0,0,7,3},//左
        {1,2,5,5,2,6}//右
    };

    public static readonly Vector2[] voxelUvs = new Vector2[6]
    {
        new Vector2(0.0f,0.0f),
        new Vector2(0.0f,1.0f),
        new Vector2(1.0f,0.0f),
        new Vector2(1.0f,0.0f),
        new Vector2(0.0f,1.0f),
        new Vector2(1.0f,1.0f)
    };
}
