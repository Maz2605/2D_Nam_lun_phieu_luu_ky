using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrityWater : Base_Item
{
    public override void Effect(Player player)
    {
        player.playerData.facingDirection = -1;
        Debug.Log("Is poisoned");
    }
}
