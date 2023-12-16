using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        NewGameManager.OnGameStateChanged += GameManagerOnGameStateChange;
    }

    private void OnDestroy()
    {
        NewGameManager.OnGameStateChanged -= GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(NewGameManager.GameState state)
    {
        if (state == NewGameManager.GameState.GameLose)
        {
            
        }
    }

    public void PlayGame()
    {
        Debug.Log("Scene changed");
        NewGameManager.Instance.UpdateGameState(NewGameManager.GameState.GameReady);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Game quited");
        Application.Quit();

    }
}
