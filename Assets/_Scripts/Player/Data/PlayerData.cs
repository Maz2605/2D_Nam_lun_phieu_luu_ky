using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/Data")]
public class PlayerData : ScriptableObject
{
    [Header("Base")]
    public int maxHealth = 100;
    public int facingDirection = 1;
    public float destroyAfterSeconds = 1f;
    [Header("Move State")]
    public float moveSpeed = 5f;
    
    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public int amountOfJumps = 2;
    
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.2f;
    
}
