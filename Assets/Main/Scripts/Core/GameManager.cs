using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;




public class GameManager : Singleton<GameManager>
{

    [SerializeField] private TextMeshProUGUI instructionText;
    private enum GameMode 
    {
        INIT,
        READY,
        PLAY, 
        PAUSE,
        GAMEOVER 
    }

    
    private GameMode currentGameMode = GameMode.INIT;

    public event EventHandler OnGameModeChanged;


    private float initialReadyCount = 3f;
    private float readyCount = 0;
    private bool isReadyToStart = false;


    private void Start()
    {
        readyCount = initialReadyCount; 
    }

    private void FixedUpdate() {
       switch(currentGameMode)
       {
           case GameMode.INIT:
               instructionText.text = "Get Ready";
 
               break;
        case GameMode.READY:

            readyCount -= Time.deltaTime; 
            int readyCountAsInt = Mathf.RoundToInt(readyCount);
            instructionText.text = readyCountAsInt.ToString();
            if (readyCountAsInt <= 0)
            {
                currentGameMode = GameMode.PLAY;
                OnGameModeChanged.Invoke(this, EventArgs.Empty);
            } 
            
            break;
        case GameMode.PLAY:
            // timer -= Time.deltaTime; 
            // if(timer <= 0)
            // {
            //     OnGameOver();
            // }
        break; 
        case GameMode.GAMEOVER:
        break;
       }
    }

    public void SetReadyMode(bool isReady)
    {

        if (isReady)
        {
            readyCount = initialReadyCount; 
            currentGameMode = GameMode.READY;
        }
        else
        {
            currentGameMode = GameMode.INIT;
            
        }
      
        OnGameModeChanged.Invoke(this, EventArgs.Empty);
    }
    public void SetGameOverMode()
    {
        currentGameMode = GameMode.GAMEOVER;
        instructionText.text = "GameOver";
        OnGameModeChanged.Invoke(this, EventArgs.Empty);
    }
    
    
    


    public bool IsInitMode()
    {
        return currentGameMode == GameMode.INIT;
    } public bool IsReady()
    {
        return currentGameMode == GameMode.READY;
    } public bool IsPlayMode()
    {
        return currentGameMode == GameMode.PLAY;
    } public bool IsGameOverMode()
    {
        return currentGameMode == GameMode.GAMEOVER;
    }
}