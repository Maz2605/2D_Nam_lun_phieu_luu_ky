using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Transform playerTransform;

    [Header("Camera Follow Settings")]
    [SerializeField] private float smoothTime = 0.2f; // Thời gian để camera đuổi theo player
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f); // Giữ camera phía sau trong không gian 2D

    [Header("Camera Limits")]
    public Vector2 minLimits;  // Giới hạn X và Y tối thiểu
    public Vector2 maxLimits;  // Giới hạn X và Y tối đa

    private Vector3 velocity = Vector3.zero;  // Biến tạm thời cho SmoothDamp

    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not found. Please assign it in the inspector.");
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = playerTransform.position + offset;

        // Clamp position theo min/max limits (X và Y)
        float clampedX = Mathf.Clamp(targetPosition.x, minLimits.x, maxLimits.x);
        float clampedY = Mathf.Clamp(targetPosition.y, minLimits.y, maxLimits.y);

        Vector3 clampedTarget = new Vector3(clampedX, clampedY, targetPosition.z);

        // Smooth follow
        transform.position = Vector3.SmoothDamp(transform.position, clampedTarget, ref velocity, smoothTime);
    }
}
