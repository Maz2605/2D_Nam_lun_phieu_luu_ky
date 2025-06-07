using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField] private List<Sprite> heartSprites = new List<Sprite>(2); 
    [SerializeField] private List<Image> heartImages = new List<Image>(3); 
    private int maxLives = 3;

    private void Start()
    {
        // Initialize hearts
        for (int i = 0; i < maxLives; i++)
        {
            heartImages[i].sprite = heartSprites[0]; 
        }
    }

    private void Update()
    {
        ChangerImage();
    }
    
    void ChangerImage()
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i < GameManager.Instance.playerLive)
            {
                heartImages[i].sprite = heartSprites[0];
            }
            else
            {
                heartImages[i].sprite = heartSprites[1];
            }
        }
    }
}
