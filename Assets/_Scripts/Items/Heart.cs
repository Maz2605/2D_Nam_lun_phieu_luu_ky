using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Heart : Base_Item 
{
    public override void Effect(Player player)
    {
        player.TakeDamage(-100);
        GameManager.Instance.AddLives();
    }
}
