using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneName
{
    Loading,
    MainMenu,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
}

public static class SceneNameExtension
{
    public static string ToSceneString(this SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Loading:
                return "Loading";
            case SceneName.Level1:
                return "Level_1";
            case SceneName.Level2:
                return "Level_2";
            case SceneName.Level3:
                return "Level_3";
            case SceneName.Level4:
                return "Level_4";
            case SceneName.Level5:
                return "Level_5";
            case SceneName.MainMenu:
                return "MainMenu";
            default:
                Debug.LogWarning($"SceneName: {sceneName} chưa được cấu hình!");
                return "UnknownScene";
        }
    }
}
