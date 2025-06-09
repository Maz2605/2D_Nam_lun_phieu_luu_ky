using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform playerTransform;

    [Header("Camera Follow Settings")] [SerializeField]
    private float smoothTime = 0.2f; 

    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f); 

    [Header("Camera Limits")] public Vector2 minLimits; 
    public Vector2 maxLimits; 

    private Vector3 velocity = Vector3.zero; 

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }

        if (playerTransform != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        float clampedX = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
        float clampedY = Mathf.Clamp(targetPosition.y, minLimits.y, maxLimits.y);

        Vector3 clampedTarget = new Vector3(clampedX, clampedY, targetPosition.z);
        
        transform.position = Vector3.SmoothDamp(transform.position, clampedTarget, ref velocity, smoothTime);
    }
}