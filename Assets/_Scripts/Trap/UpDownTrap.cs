using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownTrap : MonoBehaviour
{
    Vector2 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + new Vector2(0, 0.5f);
    }
}
