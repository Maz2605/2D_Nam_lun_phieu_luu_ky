using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Loading;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTranslate : Singleton<AnimationTranslate>
{
    [Header("Popup Prefab")]
    public GameObject popupPrefab;  // Prefab chứa Image

    [Header("Animation Settings")]
    public float zoomInDuration = 0.5f;
    public float holdDuration = 0.5f;
    public float zoomOutDuration = 0.5f;
    public Ease zoomEase = Ease.OutQuad;
    
    public float duration = 2f;
    private Action extraEvent;

    public Action ExtraEvent
    {
        set => extraEvent = value;
    }
    
    public bool IsActive { get; private set; } = false;

    public void DisplayAnim(bool enable, Action onClosed = null)
    {
        
    }
    
    public void StartAnim(Action onClosed = null)
    {
        DisplayAnim(true);
        DOVirtual.DelayedCall(duration, () =>
        {
            onClosed?.Invoke();
        });
    }

    
}
