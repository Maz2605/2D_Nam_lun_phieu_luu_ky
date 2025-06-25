using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    
    private Player _player;
    private InputManager inputManager;
    private int direction;
    
    private float _fireTimer = 0f;

    public float cooldown = 0.5f;
    private void Awake()
    { 
        inputManager = GetComponentInParent<InputManager>();
        _player = GetComponentInParent<Player>();
        direction = _player.playerData.facingDirection;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0f && inputManager.ClickInput)
        {
            _fireTimer = cooldown;
            Shoot();
        }
    }
    void Shoot()
    {
        AudioManager.Instance.PlaySfxFire();
        if(bulletPrefab == null) return;
        this.direction = _player.playerData.facingDirection;
        GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = firePoint.right.normalized * this.direction;
        bullet.GetComponent<PlayerBullet>().SetDirection(direction);
    }
}
