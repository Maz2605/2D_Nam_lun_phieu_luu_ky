using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground"))
        {
            var player = GetComponentInParent<Player>();
            player.DisableShieldVisual();
        }
    }
}
