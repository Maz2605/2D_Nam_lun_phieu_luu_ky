using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : Base_Item
{
    public override void Effect(Player player)
    {
        player.EnableGun();
    }
}
