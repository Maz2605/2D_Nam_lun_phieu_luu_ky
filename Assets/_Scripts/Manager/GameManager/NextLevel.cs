using System;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public SceneName sceneName;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.OnLevelComplete();
        }
    }
    
}
