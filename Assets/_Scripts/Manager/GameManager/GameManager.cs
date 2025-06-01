using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
    }
    
    private GameState currentGameState = GameState.WaitingChoiceLevel;

    public GameState CurrentGameState
    {
        get => CurrentGameState;
        set => CurrentGameState = value;
    }
    
    [SerializeField] private LevelController levelController;
    
    private int level;
    
    public LevelController LevelController => levelController;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Level_" + level, 1);
        currentGameState = GameState.WaitingChoiceLevel;
    }

    public void PlayGame(int indexLevel)
    {
        AnimationTranslate.Instance.StartAnim(delegate
        {
            level = indexLevel;
            AnimationTranslate.Instance.DisplayAnim(false);
            levelController.SpawnLevel(indexLevel - 1);
            currentGameState = GameState.Playing;
            
        });
        
        DOVirtual.DelayedCall(2f, delegate
        {
            //Âm Thanh

        });
    }

    public void BackHome()
    {
        AudioManager.Instance.PlaySoundButtonClick();
        CurrentGameState = GameState.WaitingChoiceLevel;
        levelController.DespawnLevel();
        //Âm thanh
    }
    
}
