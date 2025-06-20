using UnityEngine;

public class DrityWater : Base_Item
{
    public override void Effect(Player player)
    {
        player.playerData.facingDirection = -1;
        player.Anim.SetTrigger("Item2");
        Debug.Log("Is poisoned");
    }
}
