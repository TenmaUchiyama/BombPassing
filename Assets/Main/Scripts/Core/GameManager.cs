using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;




public class GameManager : Singleton<GameManager>
{

    [SerializeField] private TextMeshProUGUI instructionText;

    [SerializeField] private CountDownUI countDownUI;
    private enum GameMode 
    {
        INIT,
        READY,
        PLAY, 
        PAUSE,
        GAMEOVER 
    }

    
    private GameMode currentGameMode = GameMode.INIT;
    private GameMode previousGameMode;

    public event EventHandler OnGameModeChanged;




    private float initialReadyCount = 3f;
    private float readyCount = 0;
    private int previousReadyCount; 


    private void Start()
    {
        readyCount = initialReadyCount; 
        previousGameMode = currentGameMode;
    }

    private void FixedUpdate() {

        
        if(previousGameMode != currentGameMode)
       {
         previousGameMode = currentGameMode; 

        OnGameModeChanged.Invoke(this, EventArgs.Empty);
       }

      
    }

    public void SetReadyMode(object sender, bool isReady)
    {

        Debug.Log($"<color=yellow>Ready Set by: {sender}</color>");

        if (isReady)
        {
            readyCount = initialReadyCount; 
            currentGameMode = GameMode.READY;
        }
        else
        {
            currentGameMode = GameMode.INIT;
            readyCount = initialReadyCount;
            previousReadyCount = 0;
        }
    }
   
    public void SetPlayMode(object sender)
    {
         Debug.Log($"<color=yellow>Play Set by: {sender}</color>");
          currentGameMode = GameMode.PLAY;
         
    }
    
    
    public void SetGameOverMode(object sender)
    {

        Debug.Log($"<color=yellow>GameOver Set by: {sender}</color>");
        currentGameMode = GameMode.GAMEOVER;
        instructionText.text = "GameOver";
    }
    



    public bool IsInitMode()
    {
        return currentGameMode == GameMode.INIT;
    }
    public bool IsReady()
    {
        return currentGameMode == GameMode.READY;
    }
     public bool IsPlayMode()
    {
        return currentGameMode == GameMode.PLAY;
    } 
    public bool IsGameOverMode()
    {
        return currentGameMode == GameMode.GAMEOVER;
    }
}