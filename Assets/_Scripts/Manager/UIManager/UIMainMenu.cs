using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Panels")] [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] public GameObject levelSelectorPanel;

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
    [SerializeField] private Button level5Button;


    private void Awake()
    {
        ShowLevelSelectorPanel(true);
        ShowMainSelectorPanel(false);
        newGameButton?.onClick.AddListener(OnNewGamePressed);
        levelsButton?.onClick.AddListener(OnLevelsPanelPressed);
        quitGameButton?.onClick.AddListener(OnQuitGamePressed);
        backButton?.onClick.AddListener(OnBackPressed);
    }

    private void Start()
    {
        level1Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level1); AudioManager.Instance.PlayMusicBg1();});
        level2Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level2); });
        level3Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level3); });
        level4Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level4); });
        level5Button?.onClick.AddListener(() => { OnLevelsButtonPressed(SceneName.Level5); });

        SetLevelInteractable(level1Button, 1);
        SetLevelInteractable(level2Button, 2);
        SetLevelInteractable(level3Button, 3);
        SetLevelInteractable(level4Button, 4);
        SetLevelInteractable(level5Button, 5);
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
            SceneName.Level5 => 5,
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
}