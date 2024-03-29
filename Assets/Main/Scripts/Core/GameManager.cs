using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;




public class GameManager : Singleton<GameManager>
{



    [SerializeField] private CountDownUI countDownUI;


    private  int passCount = 0;
    public  int PassCount => passCount;
    public event EventHandler OnPassCountChanged;
    
    
    
    private enum GameMode 
    {
        INIT,
        READY,
        PLAY, 
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

            if (OnGameModeChanged != null) OnGameModeChanged.Invoke(this, EventArgs.Empty);
        }
        
      
    }

    public void SetReadyMode(object sender, bool isReady)
    {

  
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
       
          currentGameMode = GameMode.PLAY;
         
    }
    
    
    public void SetGameOverMode(object sender)
    {
      
        currentGameMode = GameMode.GAMEOVER;
        // instructionText.text = "GameOver";
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

    public void PressCountUp()
    {
        passCount += 1;
       
        if (OnPassCountChanged != null) OnPassCountChanged.Invoke(this, EventArgs.Empty);
    }
}