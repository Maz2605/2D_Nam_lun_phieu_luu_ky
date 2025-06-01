using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UILose : MonoBehaviour
{
    [SerializeField] private UIAppear dime;
    [SerializeField] private UIAppear popup;

    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 500);
    }

    public void DisplayLose(bool isLose, Action callback = null)
    {
        if (isLose)
        {
            dime.gameObject.SetActive(true);
            popup.gameObject.SetActive(true);
        }
        else
        {
            dime.gameObject.SetActive(false);
            popup.gameObject.SetActive(false);
            DOVirtual.DelayedCall(0.1f, ()=>callback?.Invoke());
        }
    }
}
