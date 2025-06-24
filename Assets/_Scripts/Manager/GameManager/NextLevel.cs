using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("Gate Sprites")]
    public Sprite activeSprite;    // Sprite khi mở cổng (đủ Sun)
    public Sprite inactiveSprite;  // Sprite khi khóa cổng (chưa đủ Sun)

    private SpriteRenderer spriteRenderer;
    private string _sceneName;
    private bool isOpen;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _sceneName = SceneManager.GetActiveScene().name;

        UpdateGateVisual();

        SunManager.OnSunCollected += UpdateGateVisual;
    }

    private void OnDestroy()
    {
        SunManager.OnSunCollected -= UpdateGateVisual;
    }

    private void UpdateGateVisual()
    {
        GameManager.Instance.saveData.sunCountPerLevel.TryGetValue(_sceneName, out int count);
        isOpen = count == 3;

        // Đổi sprite theo trạng thái
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isOpen ? activeSprite : inactiveSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            GameManager.Instance.OnLevelComplete();
        }
        else if (other.CompareTag("Player") && !isOpen)
        {
            Debug.Log("Cổng chưa mở, cần thu thập đủ Sun để mở cổng.");
        }
    }
}