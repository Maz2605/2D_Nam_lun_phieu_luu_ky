using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBomb : BaseBullet
{
    private Animator animator;
    private Collider2D targetToDamage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Effect(Collider2D collision)
    {
        targetToDamage = collision;
        StartCoroutine(DamageAfterExplosion());
    }

    private IEnumerator DamageAfterExplosion()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Bomb");
        
        yield return new WaitForSeconds(0.5f);
        
        
        base.Effect(targetToDamage);

        
        PoolingManager.Instance.Despawn(gameObject);
    }
    
    
}
