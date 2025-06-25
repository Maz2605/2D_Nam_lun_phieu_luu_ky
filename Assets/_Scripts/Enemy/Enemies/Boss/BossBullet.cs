using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BaseBullet
{
    public float amplitude = 0.5f; // Biên độ sóng
    public float frequency = 5f; // Tần số sóng

    private Vector2 perpendicular;
    private float timeElapsed;

    protected override void Update()
    {
        timeElapsed += Time.deltaTime;

        // Tạo chuyển động sóng sin trên vector vuông góc
        Vector2 forwardMove = direction * bulletData.speed * timeElapsed;
        float sinOffset = Mathf.Sin(timeElapsed * frequency) * amplitude;
        Vector2 offset = perpendicular * sinOffset;

        transform.position = startPos + forwardMove + offset;
        
        if (Vector2.Distance(startPos, transform.position) >= bulletData.maxDistance)
        {
            PoolingManager.Instance.Despawn(gameObject);
        }
    }

    public override void SetDirectionFromEnemy(int faceDirection)
    {
        base.SetDirectionFromEnemy(faceDirection);

        // Tính vector vuông góc (để tạo sóng)
        perpendicular = new Vector2(-direction.y, direction.x).normalized;
        timeElapsed = 0f;
    }
}

