using System;
using UnityEngine;




namespace BombPassing.Core{
public class GameManager : Singleton<GameManager>
{


    public enum GameMode 
    {
        PLAY, 
        GAMEOVER 
    }


    public GameMode currentGameMode = GameMode.PLAY;
    
    [SerializeField] private float timer = 5f;



    public event EventHandler onGameOverEvent;


    private void FixedUpdate() {
       switch(currentGameMode)
       {
        case GameMode.PLAY:
          
        break; 
        case GameMode.GAMEOVER:
        break;
       }
    }


    public void OnGameOver(MonoBehaviour context)
    {

        onGameOverEvent?.Invoke(this, EventArgs.Empty);
        Debug.Log("Game Over By: " + context.transform.name);
        currentGameMode = GameMode.GAMEOVER;
    }
}
}