using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Data/Entity Data/ Base Data")]
public class Data_Entity : ScriptableObject
{
    public float health = 100f;
    public float wallCheckDistance = 1.5f;
    public float ledgeCheckDistance = 1.5f;
    public float groundCheckRadius = 1.5f;
    
    public float minAgroDistance = 1.5f;
    public float maxAgroDistance = 1.5f;
    public float closeRangeActionDistance = 1.5f;
    
    public float stunResistant = 1.5f;
    public float stunRecoveryTime = 1.5f;
    
    public LayerMask playerLayer;
}

