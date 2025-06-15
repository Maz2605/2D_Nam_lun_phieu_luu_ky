using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : Singleton<SceneLoader>
{
    protected override void Awake()
    {
        base.KeepAlive(true);
        base.Awake();
    }
    
    
    public void UnLoadSceneAdditive(string sceneName, Action onSceneUnload = null, Action onSceneUnloaded = null)
    {
        if (string.IsNullOrEmpty(sceneName)) return;
            
        var scene = SceneManager.GetSceneByName(sceneName);

        if (scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);

            SceneManager.sceneUnloaded += s => { Resources.UnloadUnusedAssets(); };

            onSceneUnload?.Invoke();

            DOVirtual.DelayedCall(1f, () => { onSceneUnloaded?.Invoke(); });
        }
    }

    public void LoadScene(string sceneName)
    {   
        if (sceneName.StartsWith("Level_"))
        {
            string levelNumber = sceneName.Replace("Level_", "");
            if (int.TryParse(levelNumber, out int parsedIndex))
            {
                GameManager.Instance.SetCurrentLevelIndex(parsedIndex);
            }
        }
        DOVirtual.DelayedCall(0.5f, () => { SceneManager.LoadScene(sceneName); });
    }
}
