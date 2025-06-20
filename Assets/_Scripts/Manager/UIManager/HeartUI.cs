using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;

    [Header("UI References")]
    [SerializeField] private List<Image> heartImages;

    private int maxLives;
    private int currentLives = -1;

    private void Awake()
    {
        maxLives = heartImages.Count;
    }

    private void Start()
    {
        UpdateHearts(GameManager.Instance.PlayerLives);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerLivesChanged += UpdateHearts;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerLivesChanged -= UpdateHearts;
    }

    private void UpdateHearts(int lives)
    {
        if (lives == currentLives) return; // Không cần update nếu không có thay đổi

        currentLives = lives;

        for (int i = 0; i < maxLives; i++)
        {
            heartImages[i].sprite = i < lives ? fullHeartSprite : emptyHeartSprite;
        }
    }
}
