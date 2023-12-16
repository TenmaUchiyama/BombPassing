using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static NewGameManager Instance;

    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    
    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch(newState){
            case GameState.GameReady:
                break;
            case GameState.GamePlaying:
                break;
            case GameState.GamePaused:
                break;
            case GameState.GameLose:
                TriggerLose();
                break;
            case GameState.GameOnMenu:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void TriggerLose()
    {

    }
    public enum GameState
    {
        GameReady,
        GamePlaying,
        GamePaused,
        GameLose,
        GameOnMenu
    }
}
