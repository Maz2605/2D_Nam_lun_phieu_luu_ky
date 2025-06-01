using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    private Transform currentLevel;
    private string levelPath = "Prefabs/Levels/Level_";

    public void SpawnLevel(int index)
    {
        DespawnLevel();

        GameObject levelPrefab = Resources.Load<GameObject>(levelPath + index);
        if (levelPrefab != null)
        {
            GameObject levelInstance = Instantiate(levelPrefab, transform.position, Quaternion.identity, transform);
            currentLevel = levelInstance.transform;
        }
        else
        {
            Debug.LogError("Level prefab not found at: " + levelPath + index);
        }
    }

    public void DespawnLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }
    }
}

 
