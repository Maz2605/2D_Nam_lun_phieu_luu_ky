using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    public UISetting UISetting => FindObjectOfType<UISetting>();
    // public UILose UILose => FindObjectOfType<UILose>();
    // public UIGamePlay UIGamePlay => FindObjectOfType<UIGamePlay>();

    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
    }
}
