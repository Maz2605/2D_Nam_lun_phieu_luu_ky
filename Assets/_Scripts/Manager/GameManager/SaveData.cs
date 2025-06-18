using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveData 
{
    public List<int> unlockedLevels = new List<int>();
    public int Lives = 3;
    
    public float respawnX = 0f;
    public float respawnY = 0f;

    public Dictionary<string, Vector2Serializable> checkpoints = new();
}
[Serializable]
public struct Vector2Serializable
{
    public float x, y;

    public Vector2Serializable(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static implicit operator Vector2(Vector2Serializable v) => new Vector2(v.x, v.y);
    public static implicit operator Vector2Serializable(Vector2 v) => new Vector2Serializable(v.x, v.y);
}
