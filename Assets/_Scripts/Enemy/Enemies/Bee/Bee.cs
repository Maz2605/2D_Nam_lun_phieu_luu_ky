using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : BaseEnemies
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform detectLine;

    private float _fireTimer;
    public float cooldown = 0.5f;

    protected override void Start()
    {
        base.Start();
        _fireTimer = 0f;
    }

    protected override void Attack()
    {
        _fireTimer -= Time.deltaTime;
        if (_fireTimer <= 0f)
        {
            _fireTimer = cooldown;
            Shoot();
        }
    }

    
    protected override void DetectPlayer()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;
        RaycastHit2D raycast = Physics2D.Raycast(origin, direction, baseEnemiesData.detectRange, baseEnemiesData.playerMask);

        if (raycast.collider != null && raycast.collider.CompareTag("Player"))
        {
            CurrentState = State.Chasing;
        }
        else
        {
            CurrentState = State.Patrol;
        }

        Debug.DrawRay(origin, direction * baseEnemiesData.detectRange, Color.red);
    }
    

    void Shoot()
    {
        if(bulletPrefab == null) return;
        GameObject bullet = PoolingManager.Instance.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
