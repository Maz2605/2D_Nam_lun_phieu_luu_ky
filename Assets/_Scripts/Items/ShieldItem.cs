using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : Base_Item
{
    public override void Effect(Player player)
    {
        player.EnableShieldVisual();
    }
}
