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
            GameManager.Instance.SetRespawnPosition(spawnPoint.transform.position);
            animator.SetBool("IsActive", true);
            SaveCheckpointState();
        }
    }

    private void SaveCheckpointState()
    {
        PlayerPrefs.SetInt($"Checkpoint_{transform.position.x}_{transform.position.y}", 1);
    }

    private void LoadCheckpointState()
    {
        int saved = PlayerPrefs.GetInt($"Checkpoint_{transform.position.x}_{transform.position.y}", 0);
        if (saved == 1)
        {
            isActivated = true;
            animator.SetBool("IsActive", true);
        }
    }

    private void Start()
    {
        LoadCheckpointState();
    }
}