using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RolateTrap : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation in degrees per second

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}


