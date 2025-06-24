using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sun : Base_Item
{
    private string sceneName;

    protected override void Start()
    {
        base.Start();
        SceneName enumScene = SceneNameExtension.ToSceneName(SceneManager.GetActiveScene().name);
        sceneName = enumScene.ToSceneString();

        if (GameManager.Instance.saveData.sunCountPerLevel.TryGetValue(sceneName, out int count) && count >= 3)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Effect(Player player)
    {
        AudioManager.Instance.PlaySfxGetItem();
        var sunDict = GameManager.Instance.saveData.sunCountPerLevel;
        
        if (!sunDict.ContainsKey(sceneName)) sunDict[sceneName] = 0;
        if (sunDict[sceneName] >= 3) return;

        sunDict[sceneName]++;
        GameManager.Instance.SaveData();
        SunManager.OnSunCollected?.Invoke();
        Debug.Log("Sun collected! Current count: "+ sunDict[sceneName] + "/ 3");
    }

    
}
