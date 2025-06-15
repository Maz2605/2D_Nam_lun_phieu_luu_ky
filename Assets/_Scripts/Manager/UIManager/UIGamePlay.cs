using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Buttons")]
    [SerializeField] private Button settingButton;
    [SerializeField] private Button muteButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button homeButtonGOV;
    [SerializeField] private Button homeButtonP;
    

    private void Start()
    {
        UpdateCameraForCanvas();
        settingButton?.onClick.AddListener(OnSettingPressed);
        resumeButton?.onClick.AddListener(OnResumePressed);
        restartButton?.onClick.AddListener(OnRestartPressed);
        homeButtonGOV?.onClick.AddListener(OnHomePressed);
        homeButtonP?.onClick.AddListener(OnHomePressed);
        muteButton?.onClick.AddListener(OnMutePressed);
        playAgainButton?.onClick.AddListener(OnRestartPressed);
        ShowUIGamePlayPanel(false);
        ShowUIGameOverPanel(false);
        // AnimationTranslate.Instance.StartLoading();
    }
    private void OnEnable()
    {
        GameManager.GameOverEvent += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.GameOverEvent -= OnGameOver;
    }
    private void UpdateCameraForCanvas()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
    }

    private void OnSettingPressed()
    {
        GameManager.Instance.OnPauseGame();
        ShowUIGamePlayPanel(true);
    }

    private void OnResumePressed()
    {
        ShowUIGamePlayPanel(false);
        GameManager.Instance.OnResumeGame();
    }

    private void OnRestartPressed()
    {
        ShowUIGamePlayPanel(false);
        GameManager.Instance.OnRestartCurrentLevel();
    }

    private void OnHomePressed()
    {
        Time.timeScale = 1f;
        ShowUIGamePlayPanel(false);
        SceneLoader.Instance.LoadScene(SceneName.MainMenu.ToSceneString());
    }

    private void OnMutePressed()
    {
        
    }
    
    public void OnGameOver()
    {
        ShowUIGamePlayPanel(false);
        ShowUIGameOverPanel(true);
    }

    private void ShowUIGamePlayPanel(bool show)
    {
        gamePlayPanel.SetActive(show);
    }


    private void ShowUIGameOverPanel(bool show)
    {
        gameOverPanel.SetActive(show);
    }

    
}