using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform defaultSpawnPoint;

    private void Start()
    {
        Vector2 spawnPos = GameManager.Instance.GetRespawnPosition();
        if (spawnPos == Vector2.zero)
        {
            spawnPos = defaultSpawnPoint.position;
            GameManager.Instance.SetRespawnPosition(spawnPos);
        }

        var player = Instantiate(playerPrefab, spawnPos, Quaternion.identity).GetComponent<Player>();
        GameManager.Instance.RegisterPlayer(player);
        CameraControl.Instance.SetTarget(player.transform);
    }
}
