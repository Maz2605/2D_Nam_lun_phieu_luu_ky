using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private bool isActivated;
    [SerializeField]
    private GameObject spawnPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            AudioManager.Instance.PlaSfxGetCheckPoint();
            GameManager.Instance.SetRespawnPosition(spawnPoint.transform.position);
            GameManager.Instance.SaveData(); // ← Lưu JSON sau khi chạm checkpoint
            animator.SetBool("IsActive", true);
        }
    }
    
}