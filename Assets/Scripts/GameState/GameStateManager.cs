using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameStateData gameStateData;
    private void Start()
    {
        if (gameStateData != null)
        {
            gameStateData.onGameStateChanged += PerformTaskBasedOnGameState;
        }
    }
    private void PerformTaskBasedOnGameState(GameStateData.GameState gameState)
    {
        throw new System.NotImplementedException();
    }
}
