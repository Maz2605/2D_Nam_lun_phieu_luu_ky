using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SaveData
{
    public List<int> unlockedLevels = new();
    public int Lives = 3;

    public float respawnX = 0f;
    public float respawnY = 0f;
    
    public List<CheckpointData> checkpoints = new();
    
    [NonSerialized] public Dictionary<string, Vector2> checkpointDict = new();
    
    public List<SunCountData> sunCountList = new();
    [NonSerialized] public Dictionary<string, int> sunCountPerLevel = new();
}

[Serializable]
public struct CheckpointData
{
    public string sceneName;
    public Vector2Serializable position;

    public CheckpointData(string sceneName, Vector2 position)
    {
        this.sceneName = sceneName;
        this.position = position;
    }
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

    public static implicit operator Vector2(Vector2Serializable v) => new(v.x, v.y);
    public static implicit operator Vector2Serializable(Vector2 v) => new(v.x, v.y);
}

[Serializable]
public struct SunCountData
{
    public string sceneName;
    public int count;

    public SunCountData(string sceneName, int count)
    {
        this.sceneName = sceneName;
        this.count = count;
    }
}
