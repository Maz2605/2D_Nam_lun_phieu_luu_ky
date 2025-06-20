using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPos : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform defaultSpawnPoint;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        Vector2 spawnPos;
        if (GameManager.Instance.saveData.checkpointDict.TryGetValue(sceneName, out Vector2 savedPos))
        {
            spawnPos = savedPos;
        }
        else
        {
            spawnPos = defaultSpawnPoint.position;
            GameManager.Instance.SetRespawnPosition(spawnPos); // Lưu mặc định
            GameManager.Instance.SaveData();
        }

        var player = Instantiate(playerPrefab, spawnPos, Quaternion.identity).GetComponent<Player>();
        GameManager.Instance.RegisterPlayer(player);

        var camera = FindObjectOfType<CameraControl>();
        camera?.SetTarget(player.transform);
    }
}
