using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game State Data")]
public class GameStateData : ScriptableObject
{
    public delegate void OnGameStateChangedEvent(GameState gameState);
    public event OnGameStateChangedEvent onGameStateChanged;
    public enum GameState { Unknown, Menu, Live, Finished }

#if UNITY_EDITOR
    [ReadOnly, SerializeField]
#endif
    private GameState currentState;
    public GameState CurrentState
    {
        get { return currentState; }
        set
        {
            if (currentState != value)
            {
                currentState = value;
                if (onGameStateChanged != null)
                {
                    onGameStateChanged(currentState);
                }
            }
        }
    }
}
