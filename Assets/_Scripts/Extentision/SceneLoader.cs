using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private Slider loading;
    public float timer;

    private string sceneName
    {
        get => PlayerPrefs.GetString("sceneName", "GamePlay");
        set => PlayerPrefs.SetString("sceneName", value);
    }

    protected override void Awake()
    {
        base.KeepAlive(true);
        base.Awake();
    }

    private void Start()
    {
        IntoGamePlay();
    }

    public void IntoGamePlay()
    {
        LoadScene(sceneName);
    }

    public void LoadSceneAdditive(string sceneName, Action onLoad = null, Action onDone = null)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (scene != null)
        {
            scene.allowSceneActivation = false;
                
            do
            {
                //nothing
            } while (scene.progress < 0.9f);
                
            onLoad?.Invoke();
            DOVirtual.DelayedCall(1f, () =>
            {
                scene.allowSceneActivation = true;
                onDone?.Invoke();
            });
        }
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
        var scene = SceneManager.LoadSceneAsync(sceneName);
        if (scene != null)
        { 
            scene.allowSceneActivation = false;

            loading.DOValue(1f, timer).SetEase(Ease.Linear).OnComplete(delegate
            {
                AnimationTranslate.Instance.StartAnim(delegate
                {
                    AnimationTranslate.Instance.DisplayAnim(false);
                    scene.allowSceneActivation = true;
                });
            });
        }
    }
}
