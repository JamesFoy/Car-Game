using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenBehaviour : MonoBehaviour
{
    public GameObject finishObject;
    private void Start()
    {
        if (GameStateManager.Instance != null)
        {
            if (GameStateManager.Instance.gameStateData.CurrentState != GameStateData.GameState.Finished)
            {
                finishObject.SetActive(false);
            }
        }
    }
    public void GameFinished()
    {
        finishObject.SetActive(true);
    }

    public void OnMainMenuTriggered()
    {
        SceneManager.LoadScene("MainMenu_Assembly");
    }
}
