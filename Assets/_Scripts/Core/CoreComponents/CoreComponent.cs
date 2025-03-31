using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core Core;

    protected virtual void Awake()
    {
        this.Core = transform.parent.GetComponent<Core>();

        if (this.Core == null)
        {
            Debug.LogError("No Core On the parent name: " + transform.parent.name);
        }
        
        this.Core.AddCoreComponent(this);
    }

    public virtual void LogicUpdate()
    {
        
    }
}
