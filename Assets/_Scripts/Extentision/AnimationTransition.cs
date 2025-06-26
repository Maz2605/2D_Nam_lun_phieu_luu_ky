using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimationTransition : Singleton<AnimationTransition>
{
    [Header("Transition Settings")]
    public Image transitionImage;           
    public float zoomDuration = 1.2f;       
    public string sceneToLoad; 

    protected override void Awake()
    {
        KeepAlive(true);
        base.Awake();
    }

    public void OnPlay(string sceneName = "MainMenu")
    {
        sceneToLoad = sceneName;
        transitionImage.gameObject.SetActive(true);
        transitionImage.transform.localScale = Vector3.one; 
        StartCoroutine(PlayTransition());
    }

    IEnumerator PlayTransition()
    {
        yield return new WaitForSeconds(0.2f); // Delay nhẹ trước hiệu ứng
        
        transitionImage.transform.DOScale(40f, zoomDuration).SetEase(Ease.InQuad)
            .OnComplete(() =>
            {
                // SceneManager.LoadScene(sceneToLoad);
                transitionImage.transform.localScale = Vector3.one; 
                transitionImage.gameObject.SetActive(false); // Tắt hình ảnh sau khi chuyển cảnh
            });
    }
}
