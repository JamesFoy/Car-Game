using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject pauseObject;
    bool isPaused;

    private void Start()
    {
        PauseGame(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                PauseGame(true);
            }
            else
            {
                PauseGame(false);
            }
        }
    }

    void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            pauseObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnMainMenuTriggered()
    {
        SceneManager.LoadScene("MainMenu_Assembly");
    }
}
