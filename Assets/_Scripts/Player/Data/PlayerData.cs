using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Data")]
    public float health;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
}
