using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Base", fileName = "BaseEnemiesData")]
public class BaseEnemiesData : ScriptableObject
{
    public int facingDirection = 1;
    
    [Header("Health")]
    public int health = 100;
    public float destroyAfter = 1;
    
    [Header("Attack")]
    public float detectRange = 2.5f;
    public int knockbackForce = 20;
    public float attackCooldown = 1f;
    public int damage = 20;
    public LayerMask playerMask;
    
    [Header("Movement")]
    public int moveSpeed = 2;
    public int patrolRange = 2;
}
