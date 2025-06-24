using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Base_Item : MonoBehaviour
{

    [Header("Scale Settings")] public float scaleAmount = 1.3f;
    public float scaleDuration = 1f;

    private Vector3 initialPos;
    private Vector3 initialScale;

    protected virtual void Start()
    {
        AnimateScaling();
    }

    

    protected bool IsTrigger = false;
    public abstract void Effect(Player player);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !IsTrigger)
        {
            AudioManager.Instance.PlaySfxGetItem();
            Effect(other.gameObject.GetComponent<Player>());
            IsTrigger = true;
            gameObject.SetActive(false);
        }
    }
    

    void AnimateScaling()
    {
        transform.DOScale(Vector3.one * scaleAmount, scaleDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}