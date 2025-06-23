using DG.Tweening;
using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float maxHeight = 5f;
    public float delayBetweenJumps = 2f;
    public float rotationAmount = 180f;
    public float rotationDuration = 0.5f;

    private Rigidbody2D rb;
    private float startY;
    private int _damage = 100; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;

        StartCoroutine(JumpLoop());
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            yield return new WaitUntil(() => transform.position.y >= startY + maxHeight);
            yield return transform.DORotate(
                new Vector3(0, 0, transform.eulerAngles.z + rotationAmount),
                rotationDuration,
                RotateMode.FastBeyond360
            ).WaitForCompletion();

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            
            yield return new WaitUntil(() => transform.position.y <= startY + 0.1f && rb.velocity.y <= 0);
            
            yield return transform.DORotate(
                new Vector3(0, 0, transform.eulerAngles.z + rotationAmount),
                rotationDuration,
                RotateMode.FastBeyond360
            ).WaitForCompletion();
            rb.velocity = new Vector2(rb.velocity.x, transform.position.y);
            yield return new WaitForSeconds(delayBetweenJumps);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var dg = other.gameObject.GetComponent<IDamageable>();
            if (dg != null && rb != null)
            {
                dg.TakeDamage(_damage);
            }
        }
    }
}