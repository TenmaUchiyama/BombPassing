using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;




public class GameManager : Singleton<GameManager>
{



    [SerializeField] private CountDownUI countDownUI;
    [SerializeField] private DataHolder dataHolder;

    private  int passCount = 0;
    public  int PassCount => passCount;
    public event EventHandler OnPassCountChanged;
    
    
    
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
        if (OnGameModeChanged != null) OnGameModeChanged += OnGameModeChangedFunc;
    }

    private void OnGameModeChangedFunc(object sender, EventArgs e)
    {
        if (currentGameMode == GameMode.GAMEOVER)
        {
            dataHolder.SetPassCountData(this.passCount);
        }
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
        Debug.Log($"<color=yellow>Play Set by: {sender}</color>");
        dataHolder.SetPassCountData(this.passCount);
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
        if (passCount == null) Debug.Log("It is null");
        // instructionText.text = passCount.ToString();
        if (OnPassCountChanged != null) OnPassCountChanged.Invoke(this, EventArgs.Empty);
    }
}