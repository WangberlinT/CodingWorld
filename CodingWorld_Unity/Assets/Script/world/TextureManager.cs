using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureManager
{
    public enum Block
    {
        Stone = 0, Grass_Dirt = 1
    }

    static MeshRenderer renderer;

    public static Material GetMaterial(Block type)
    {
        return renderer.materials[(int)type];
    }
}
