using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;
    private Vector2 currentRespawnPosition;

    private int maxLives = 3;
    public int PlayerLives { get; private set; }

    public event Action<int> OnPlayerLivesChanged;

    protected override void Awake()
    {
        KeepAlive(true);
        base.Awake();
        PlayerLives = maxLives;
    }

    public void PlayerDied()
    {
        PlayerLives = Mathf.Clamp(PlayerLives - 1, 0, maxLives);
        OnPlayerLivesChanged?.Invoke(PlayerLives);

        if (PlayerLives > 0)
        {
            RespawnPlayer();
        }
        else
        {
            Debug.Log("Game Over!");
        }
    }

    private void RespawnPlayer()
    {
        Vector2 spawnPosition = GetRespawnPosition();
        if (spawnPosition == Vector2.zero)
        {
            spawnPosition = Vector2.zero; 
        }
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        
    }
    public void AddLives()
    {
        PlayerLives = Mathf.Clamp(PlayerLives + 1, 0, maxLives);
        OnPlayerLivesChanged?.Invoke(PlayerLives);
    }
    
    public void SetRespawnPosition(Vector2 respawnPosition)
    {
        currentRespawnPosition = respawnPosition;
    }

    public Vector2 GetRespawnPosition()
    {
        return currentRespawnPosition;
    }
}