using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StaticEnemiesData", menuName = "Data/Static Enemies Data")]
public class BaseStaticEnemyData : ScriptableObject 
{
    [Header("Base")]
    public int facingDirection = 1;
    [Header("Health")]
    public int health = 100;
    public float destroyAfterSeconds = 1f;
    
    [Header("Attack Settings")]
    public float detectionRange = 3.0f;
    public float attackCooldown = 0.5f;
    public LayerMask playerMask;
}
