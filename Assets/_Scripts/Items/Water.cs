using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Base_Item
{
    public override void Effect(Player player)
    {
        player.playerData.facingDirection = 1;
        Debug.Log("Water Effect");
    }
}
