using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Boss : BaseEnemies
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    private int _bulletsFired;
    public float currentFrequency;
    public float currentAmplitude;
    private float _attackTimer = 0f;

    protected override void Start()
    {
        base.Start();
        _bulletsFired = 0;
    }

    protected override void Patrol()
    {
        Rb.velocity = Vector2.zero;
    }

    protected override void Attack()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0f)
        {
            _attackTimer = baseEnemiesData.attackCooldown;
            Shoot();
            
            float jumpHeight = 1f;
            float duration = 0.5f;
            
            transform.DOKill(); 
            
            transform.DOBlendableMoveBy(Vector3.up * jumpHeight, duration / 2)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    transform.DOBlendableMoveBy(Vector3.down * jumpHeight, duration / 2)
                        .SetEase(Ease.InQuad);
                });
        }
    }

    protected void Shoot()
    {
        GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(0, 0, 180f));

      
        var bossBullet = bullet.GetComponent<BossBullet>();
        bossBullet?.SetDirectionFromEnemy(faceDirection);
        if (_bulletsFired % 10 == 0)
        {
            currentFrequency = Random.Range(2f, 8f);
            currentAmplitude = Random.Range(0.3f, 0.7f);
        }

        if (bossBullet != null) bossBullet.frequency = currentFrequency;
        if (bossBullet != null) bossBullet.amplitude = currentAmplitude;

        _bulletsFired++;
    }
}