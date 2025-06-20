using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trunk : BaseEnemies
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private float _fireTimer = 0f;
    public float cooldown = 0.5f;

    public override void Attack()
    {
        base.Attack();
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0f)
        {
            _fireTimer = cooldown;
            Shoot();
        }
    }

    void Shoot()
    {
        if(bulletPrefab == null) return;
        GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<BaseBullet>().SetDirectionFromEnemy(faceDirection);
    }
}
