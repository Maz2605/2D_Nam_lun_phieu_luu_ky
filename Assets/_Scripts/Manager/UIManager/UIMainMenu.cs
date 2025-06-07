using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Panels")] public GameObject mainMenuPanel;
    public GameObject levelSelectorPanel;
    [Header("Buttons")] private Button newGameButton;
    private Button levelsButton;
    private Button quitGameButton;
    private Button backButton;

    [Header("Button Levels")] private Button level1Button;
    private Button level2Button;
    private Button level3Button;
    private Button level4Button;
    private Button level5Button;


    private void Awake()
    {
        mainMenuPanel.transform.localScale = Vector3.zero;
        mainMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        newGameButton = GameObject.Find("BtnPlayNewGame")?.GetComponent<Button>();
        levelsButton = GameObject.Find("BtnChooseLevel")?.GetComponent<Button>();
        quitGameButton = GameObject.Find("BtnQuit")?.GetComponent<Button>();
        backButton = GameObject.Find("BtnBack")?.GetComponent<Button>();

        newGameButton?.onClick.AddListener(() => OnLevelsButtonPressed(SceneName.Level1));
        levelsButton?.onClick.AddListener(OnLevelsPanelPressed);
        quitGameButton?.onClick.AddListener(OnQuitGamePressed);
        backButton?.onClick.AddListener(OnBackPressed);

        // Auto-assign Level Buttons
        foreach (Button btn in levelSelectorPanel.GetComponentsInChildren<Button>())
        {
            if (btn.name.StartsWith("BtnLevel_"))
            {
                int levelIndex = int.Parse(btn.name.Split('_')[1]);
                SceneName sceneName = (SceneName)levelIndex;
                btn.onClick.AddListener(() => OnLevelsButtonPressed(sceneName));

                // Check unlock (optional)
                btn.interactable = IsLevelUnlocked(levelIndex);
            }
        }

        levelSelectorPanel.SetActive(false);
    }


    public void OnLevelsButtonPressed(SceneName sceneName)
    {
        SceneLoader.Instance.LoadScene(sceneName.ToSceneString());
    }

    public void OnLevelsPanelPressed()
    {
        mainMenuPanel.SetActive(false);
        levelSelectorPanel.SetActive(true);

        levelSelectorPanel.transform.localScale = Vector3.zero;
        levelSelectorPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void OnQuitGamePressed()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OnBackPressed()
    {
        levelSelectorPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        mainMenuPanel.transform.localScale = Vector3.zero;
        mainMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    private bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex == 1)
            return true;

        int previousLevel = levelIndex - 1;
        return PlayerPrefs.GetInt("Level_" + previousLevel + "_Completed", 0) == 1;
    }
}