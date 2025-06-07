using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public int playerLive = 3;

    private bool isProtect = false;
    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
    }

    private void Start()
    {
        isProtect = false;
    }
}
