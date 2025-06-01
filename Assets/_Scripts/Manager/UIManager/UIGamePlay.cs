using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject game;

    public void ShowDisplayHome()
    {
        home.SetActive(true);
        game.SetActive(false);
    }

    public void ShowDisplayGame()
    {
        home.SetActive(false);
        game.SetActive(true);
    }
}
