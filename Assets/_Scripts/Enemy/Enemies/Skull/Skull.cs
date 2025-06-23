using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skull : BaseEnemies
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float cooldown = 3f;
    [SerializeField] private int bulletCount = 20;

    [SerializeField] private GameObject protect;
    
    private float _fireTimer = 0f;
    
    protected override void Attack()
    {
        base.Attack();
        SetActiveProtect();
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0f)
        {
            _fireTimer = cooldown;
            ShootCircle();
        }
    }
    private void ShootCircle()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * 360f / bulletCount;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = dir * bulletSpeed;
        }
    }

    protected override void Patrol()
    {
        base.Patrol();
        SetInactiveProtect();
    }

    public void SetActiveProtect() => protect?.SetActive(true);
    public void SetInactiveProtect() => protect?.SetActive(false);
}