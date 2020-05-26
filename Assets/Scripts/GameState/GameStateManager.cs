using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public GameStateData gameStateData;
    [SerializeField] GameObject winScreen;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        if (gameStateData != null)
        {
            gameStateData.onGameStateChanged += PerformTaskBasedOnGameState;
            gameStateData.CurrentState = GameStateData.GameState.Unknown;
        }
    }
    private void Update()
    {
    }
    private void PerformTaskBasedOnGameState(GameStateData.GameState gameState)
    {
        Debug.Log("gamestate changed to " + gameState.ToString());
        if (gameState == GameStateData.GameState.Finished)
        {

        }
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
