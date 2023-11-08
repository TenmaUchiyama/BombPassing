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
            timer -= Time.deltaTime; 
            if(timer <= 0)
            {
                OnGameOver();
            }
        break; 
        case GameMode.GAMEOVER:
        break;
       }
    }


    public void OnGameOver()
    {
        onGameOverEvent?.Invoke(this, EventArgs.Empty);
        Debug.Log("Game Over");
        currentGameMode = GameMode.GAMEOVER;
    }
}
}