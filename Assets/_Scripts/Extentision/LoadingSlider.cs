using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSlider : MonoBehaviour
{
    [SerializeField]
    private Slider loadingSlider;
    [SerializeField]
    private TextMeshProUGUI percentageText;
    public float loadingDuration = 20f;
    public float extraDelay = 0.5f;  
    public string nextSceneName = "MainMenu";

    private void Start()
    {
        loadingSlider.value = 0f;
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        loadingSlider.DOValue(1f, loadingDuration).SetEase(Ease.InOutQuad);
        float displayedPercentage = 0f;
        DOTween.To(() => displayedPercentage, x => displayedPercentage = x, 100f, loadingDuration)
            .SetEase(Ease.InOutQuad)
            .OnUpdate(() =>
            {
                percentageText.text = $"{displayedPercentage:F0}%";
            });
        yield return new WaitForSeconds(loadingDuration);
        yield return new WaitForSeconds(extraDelay);
        SceneManager.LoadScene(nextSceneName);
        }
    }

