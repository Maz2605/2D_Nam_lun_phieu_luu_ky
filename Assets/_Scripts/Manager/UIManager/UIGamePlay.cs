using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Buttons")]
    [SerializeField] private Button settingButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button homeButtonGOV;
    [SerializeField] private Button homeButtonP;
    
    [Header("Audio Settings")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private readonly float[] _volumeValues = {0f, 0.25f, 0.5f, 0.75f, 1f};
    private void Start()
    {
        UpdateCameraForCanvas();
        settingButton?.onClick.AddListener(OnSettingPressed);
        resumeButton?.onClick.AddListener(OnResumePressed);
        restartButton?.onClick.AddListener(OnRestartPressed);
        homeButtonGOV?.onClick.AddListener(OnHomePressed);
        homeButtonP?.onClick.AddListener(OnHomePressed);
        playAgainButton?.onClick.AddListener(OnRestartPressed);
        
        musicSlider.wholeNumbers = true;
        musicSlider.minValue = 0;
        musicSlider.maxValue = 4;
        musicSlider.value = GetStepIndex(AudioManager.Instance.MusicVolume);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        
        sfxSlider.wholeNumbers = true;
        sfxSlider.minValue = 0;
        sfxSlider.maxValue = 4;
        sfxSlider.value = GetStepIndex(AudioManager.Instance.SfxVolume);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        
        ShowUIGamePlayPanel(false);
        ShowUIGameOverPanel(false);
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
        AudioManager.Instance.PlaySfxButtonClick();
        GameManager.Instance.OnPauseGame();
        ShowUIGamePlayPanel(true);
    }

    private void OnResumePressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        ShowUIGamePlayPanel(false);
        GameManager.Instance.OnResumeGame();
    }

    private void OnRestartPressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        ShowUIGamePlayPanel(false);
        GameManager.Instance.OnRestartCurrentLevel();
    }

    private void OnHomePressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        Time.timeScale = 1f;
        ShowUIGamePlayPanel(false);
        SceneLoader.Instance.LoadScene(SceneName.MainMenu.ToSceneString());
    }

    private void OnMusicVolumeChanged(float index)
    {
        int stepIndex = Mathf.RoundToInt(index);
        float volume = _volumeValues[stepIndex];
        AudioManager.Instance.SetMusicVolume(volume);
    }

    private void OnSfxVolumeChanged(float index)
    {
        int stepIndex = Mathf.RoundToInt(index);
        float volume = _volumeValues[stepIndex];
        AudioManager.Instance.SetSfxVolume(volume);
    }

    private int GetStepIndex(float volume)
    {
        float minDiff = Mathf.Abs(_volumeValues[0] - volume);
        int closestIndex = 0;
        for (int i = 1; i < _volumeValues.Length; i++)
        {
            float diff = Mathf.Abs(_volumeValues[i] - volume);
            if (diff < minDiff)
            {
                minDiff = diff;
                closestIndex = i;
            }
        }
        return closestIndex;
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