using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Item : MonoBehaviour 
{
    protected bool IsTrigger = false;
    public abstract void Effect(Player player);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !IsTrigger)
        {
            Effect(other.gameObject.GetComponent<Player>());
            Destroy(this.gameObject);
            IsTrigger = true;
        }
    }
}
