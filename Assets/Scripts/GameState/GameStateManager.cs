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
    private void Update()
    {
    }
    private void PerformTaskBasedOnGameState(GameStateData.GameState gameState)
    {
        Debug.Log("gamestate changed to " + gameState.ToString());
    }
    public void CountRemainingCars()
    {
        StartCoroutine(DelayedCarCount());
    }
    IEnumerator DelayedCarCount()
    {
        yield return null;
        List<GameObject> remainingCars = new List<GameObject>();
        remainingCars.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        if (remainingCars.Count <= 1)
        {
            gameStateData.CurrentState = GameStateData.GameState.Finished;
        }
    }
}
