using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/Data")]
public class PlayerData : ScriptableObject
{
    [Header("Player Data")]
    private float health;
    private float moveSpeed;
    private float jumpForce;
}
