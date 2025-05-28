using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Data/Bullet Data")]

public class BaseBulletData : ScriptableObject
{
    public int damage = 20;
    public float speed = 5f;
    public float maxDistance = 10f;
    public float knockbackForce;
}
