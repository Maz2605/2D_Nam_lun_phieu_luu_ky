using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Loading;
using UnityEngine;

public class AnimationTranslate : Singleton<AnimationTranslate>
{
    [SerializeField] private GameObject animation;
    [SerializeField] private SpriteMask spriteMask;
    [SerializeField] private SpriteRenderer mushroom;
    
    [SerializeField] private float duration;
    [SerializeField] private float speed;
    private Action extraEvent;

    public Action ExtraEvent
    {
        set => extraEvent = value;
    }
    
    public bool IsActive { get; private set; } = false;

    public void DisplayAnim(bool enable, Action onClosed = null)
    {
        if (enable)
        {
            mushroom.transform.localScale = Vector3.zero;
            spriteMask.transform.localScale = Vector3.zero;
            
            IsActive = true;
            animation.SetActive(true);
            var sequence = DOTween.Sequence();
            
            sequence.Append(mushroom.transform.DOScale(Vector3.one * speed, duration).SetEase(Ease.OutQuart));
        }
        else
        {
            spriteMask.transform.localScale = Vector3.zero;
            mushroom.transform.localScale = Vector3.one * speed;

            spriteMask.transform.DOScale(Vector3.one * speed, duration).SetEase(Ease.InQuart).OnComplete(() =>
            {
                onClosed?.Invoke();

                IsActive = false;
                animation.SetActive(false);
            });
        }
    }
    public void Loading(Action onLoading = null, Action onClosed = null)
    {
        DisplayAnim(true);

        DOVirtual.DelayedCall(duration, () => { onLoading?.Invoke(); });

        DOVirtual.DelayedCall(3 * duration, () =>
        {
            DisplayAnim(false, () =>
            {
                onClosed?.Invoke();
                extraEvent?.Invoke();
                extraEvent = null;
            });
        });
    }
    public void StartAnim(Action onClosed = null)
    {
        DisplayAnim(true);
        DOVirtual.DelayedCall(duration, () =>
        {
            onClosed?.Invoke();
        });
    }

    public void StopAnim(Action onClosed = null)
    {
        DOVirtual.DelayedCall(duration, () =>
        {
            onClosed?.Invoke();
            extraEvent?.Invoke();
            extraEvent = null;
        });
    }
}
