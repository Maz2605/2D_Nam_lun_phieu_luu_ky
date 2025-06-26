using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Panels")] 
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] public GameObject levelSelectorPanel;
    [SerializeField] private GameObject gamePlayPanel;


    [Header("Main Menu Buttons")] [SerializeField]
    private Button newGameButton;

    [SerializeField] private Button levelsButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button backButton;

    [Header("Level Buttons")] [SerializeField]
    private Button level1Button;

    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button level4Button;
    // [SerializeField] private Button level5Button;
    
    [Header("Setting Button")]
    [SerializeField] private Button settingButton;
    
    [Header("Audio Settings")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private readonly float[] _volumeValues = {0f, 0.25f, 0.5f, 0.75f, 1f};

    protected  void Awake()
    {
        ShowLevelSelectorPanel(true);
        ShowMainSelectorPanel(false);
        newGameButton?.onClick.AddListener(OnNewGamePressed);
        levelsButton?.onClick.AddListener(OnLevelsPanelPressed);
        quitGameButton?.onClick.AddListener(OnQuitGamePressed);
        backButton?.onClick.AddListener(OnBackPressed);
        settingButton?.onClick.AddListener(OnSettingPressed);

    }

    private void Start()
    {
        level1Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level1);});
        level2Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level2); });
        level3Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level3); });
        level4Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level4); });
        // level5Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level5); });

        SetLevelInteractable(level1Button, 1);
        SetLevelInteractable(level2Button, 2);
        SetLevelInteractable(level3Button, 3);
        SetLevelInteractable(level4Button, 4);
        // SetLevelInteractable(level5Button, 5);
        
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
    }

    private void OnEnable()
    {
        ShowLevelSelectorPanel(false);
        ShowMainSelectorPanel(true);
    }

    public void OnNewGamePressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        GameManager.Instance.ResetData();
        GameManager.Instance.OnNewGame();
    }

    public void OnLevelsButtonPressed(SceneName sceneName)
    {
        int levelIndex = sceneName switch
        {
            SceneName.Level1 => 1,
            SceneName.Level2 => 2,
            SceneName.Level3 => 3,
            SceneName.Level4 => 4,
            // SceneName.Level5 => 5,
            _ => 1
        };
        AudioManager.Instance.PlaySfxButtonClick();
        GameManager.Instance.SetCurrentLevelIndex(levelIndex);
        SceneLoader.Instance.LoadScene(sceneName.ToSceneString());
    }

    public void OnLevelsPanelPressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        ShowMainSelectorPanel(false);
        ShowLevelSelectorPanel(true);
    }

    public void OnQuitGamePressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void OnBackPressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        ShowLevelSelectorPanel(false);
        ShowMainSelectorPanel(true);
    }

    private void SetLevelInteractable(Button btn, int levelIndex)
    {
        if (btn != null)
        {
            btn.interactable = GameManager.Instance.IsLevelUnlocked(levelIndex);
        }
    }

    private void ShowMainSelectorPanel(bool show)
    {
        mainMenuPanel.SetActive(show);
        if (show)
        {
            AudioManager.Instance.PlaySfxPopUp();
            mainMenuPanel.transform.localScale = Vector3.zero;
            mainMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }
        else
        {
            mainMenuPanel.transform.localScale = Vector3.zero;
            mainMenuPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBack);
        }
    }

    private void ShowLevelSelectorPanel(bool show)
    {
        levelSelectorPanel.SetActive(show);
        if (show)
        {
            AudioManager.Instance.PlaySfxPopUp();
            levelSelectorPanel.transform.localScale = Vector3.zero;
            levelSelectorPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }
        else
        {
            levelSelectorPanel.transform.localScale = Vector3.zero;
            levelSelectorPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBack);
        }
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
    private void OnSettingPressed()
    {
        AudioManager.Instance.PlaySfxButtonClick();
        ShowUIGamePlayPanel(true);
    }
    private void ShowUIGamePlayPanel(bool show)
    {
        gamePlayPanel.SetActive(show);
    }

}