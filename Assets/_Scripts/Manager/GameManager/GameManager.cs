using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private GameObject playerPrefab;
    private Player _playerInstance;
    private Vector2 _currentRespawnPosition;
    private int _currentLevelIndex;
    private readonly string _fileName = "SaveData.json";
    private string _savePath;
    [FormerlySerializedAs("_maxLives")] public int maxLives = 3;
    public SaveData saveData;
    
    public int PlayerLives { get; private set; }

    public event Action<int> OnPlayerLivesChanged;
    public static event Action GameOverEvent;

    protected override void Awake()
    {
        KeepAlive(true);
        base.Awake();
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.StartsWith("Level_"))
        {
            string levelNumber = sceneName.Replace("Level_", "");
            if (int.TryParse(levelNumber, out int parsedIndex))
            {
                _currentLevelIndex = parsedIndex;
            }
        }
        _savePath = Path.Combine(Application.persistentDataPath, _fileName);
        LoadData();
        PlayerLives = Mathf.Clamp(saveData.Lives, 1, maxLives);
        saveData.Lives = PlayerLives;
        SaveData();
    }
    public void RegisterPlayer(Player player)
    {
        _playerInstance = player;
    }

    public void PlayerDied()
    {
        PlayerLives = Mathf.Clamp(PlayerLives - 1, 0, maxLives);
        OnPlayerLivesChanged?.Invoke(PlayerLives);
        SaveData();

        if (PlayerLives > 0)
        {
            RespawnPlayer();
        }
        else
        {
            Debug.Log("Game Over!");
            GameOverEvent?.Invoke();
            OnPauseGame();
        }
    }

    private void RespawnPlayer()
    {
        if (_playerInstance == null)
        {
            Debug.LogWarning("Player instance not registered.");
            return;
        }

        _playerInstance.transform.position = _currentRespawnPosition;
        _playerInstance.gameObject.SetActive(true);
        _playerInstance.ResetPlayer();
    }

    public void OnNewGame()
    {
        PlayerLives = maxLives;
        saveData.Lives = maxLives;
        SaveData();
        SceneLoader.Instance.LoadScene("Level_1");
    }
    public void OnPauseGame()
    {
        Time.timeScale = 0.0f;
        _playerInstance.DisablePlayer();
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1.0f;
        _playerInstance.ResetPlayer();
    }

    public void OnRestartCurrentLevel()
    {
        if (PlayerLives == 0)
        {
            AddLives();
        }
        
        Time.timeScale = 1.0f;
        SceneLoader.Instance.LoadScene("Level_" + _currentLevelIndex);
    }

    public void OnLevelComplete()
    {
        _currentLevelIndex = Mathf.Clamp(_currentLevelIndex + 1, 1, 5);
        UnLockLevel(_currentLevelIndex);
        SceneLoader.Instance.LoadScene("Level_"+ _currentLevelIndex);
    }

    public void AddLives()
    {
        PlayerLives = Mathf.Clamp(PlayerLives + 1, 0, maxLives);
        OnPlayerLivesChanged?.Invoke(PlayerLives);
    }

    public void SetRespawnPosition(Vector2 respawnPosition)
    {
        _currentRespawnPosition = respawnPosition;
        string sceneName = SceneManager.GetActiveScene().name;
        saveData.checkpoints[sceneName] = respawnPosition;
    }

    public Vector2 GetRespawnPosition()
    {
        return _currentRespawnPosition;
    }

    private void UnLockLevel(int levelIndex)
    {
        if (!saveData.unlockedLevels.Contains(levelIndex))
        {
            saveData.unlockedLevels.Add(levelIndex);
            SaveData();
            Debug.Log($"Level {levelIndex} unlocked!");
        }
    }
    public bool IsLevelUnlocked(int levelIndex)
    {
        return levelIndex == 1 || saveData.unlockedLevels.Contains(levelIndex);
    }

    public void SaveData()
    {
        saveData.Lives = PlayerLives;
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(_savePath, json);
        Debug.Log(_savePath);
        Debug.Log("Saved Data");
    }

    public void LoadData()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            PlayerLives = saveData.Lives;
            string sceneName = SceneManager.GetActiveScene().name;
            if (saveData.checkpoints.ContainsKey(sceneName))
            {
                _currentRespawnPosition = saveData.checkpoints[sceneName];
            }
            else
            {
                _currentRespawnPosition = Vector2.zero;
            }
            Debug.Log("Loaded Data");
        }
        else
        {
            saveData = new SaveData();
            saveData.unlockedLevels.Add(1);
            PlayerLives = saveData.Lives;
            SaveData();
        }
    }
    public void ResetData()
    {
        if (File.Exists(_savePath))
        {
            File.Delete(_savePath);
            Debug.Log("SaveData reset.");
        }

        saveData = new SaveData();
        saveData.unlockedLevels.Add(1);
        saveData.Lives = maxLives;
        PlayerLives = maxLives; 
        SaveData();
    }
    public void SetCurrentLevelIndex(int levelIndex)
    {
        _currentLevelIndex = levelIndex;
    }
}