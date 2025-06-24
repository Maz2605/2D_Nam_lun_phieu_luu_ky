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
    private float _jumpDuration; // Duration of the jump animation

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startY = transform.position.y;
        
        float gravity = Mathf.Abs(rb.gravityScale * Physics2D.gravity.y);
        _jumpDuration = jumpForce / gravity;
        StartCoroutine(JumpLoop());
    }

    IEnumerator JumpLoop()
    {
        while (true)
        {
            // Nhảy lên
            rb.velocity = new Vector2(0f, jumpForce);

            // Đợi tới khi lên đỉnh (vận tốc y gần bằng 0 và đang đi xuống)
            yield return new WaitUntil(() => rb.velocity.y <= 0.01f);

            // Tạm dừng lực để quay
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            yield return transform.DORotate(
                new Vector3(0, 0, transform.eulerAngles.z + rotationAmount),
                rotationDuration,
                RotateMode.FastBeyond360
            ).WaitForCompletion();

            rb.isKinematic = false;

            // Đợi rơi xuống gần vị trí ban đầu
            yield return new WaitUntil(() =>
                rb.velocity.y <= 0 && transform.position.y <= startY + 0.05f
            );

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            yield return transform.DORotate(
                new Vector3(0, 0, transform.eulerAngles.z + rotationAmount),
                rotationDuration,
                RotateMode.FastBeyond360
            ).WaitForCompletion();

            rb.isKinematic = false;

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