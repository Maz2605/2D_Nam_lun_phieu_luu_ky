using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepawnPlayerManager : Singleton<RepawnPlayerManager>
{
    
    protected override void Awake()
    {
        base.Awake();
        KeepAlive(false);
    }
    
    private Vector2 currentRespawnPosition;

    public void SetRespawnPosition(Vector2 respawnPosition)
    {
        currentRespawnPosition = respawnPosition;
    }

    public Vector2 GetRespawnPosition()
    {
        return currentRespawnPosition;
    }
}
