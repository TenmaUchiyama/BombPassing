using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombSceneManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnGameModeChanged += onGameModeChanged;
     
    }


    private void onGameModeChanged(object sender, EventArgs e)
    {
        
        if(GameManager.Instance.IsGameOverMode()) this.LoadGameOver();
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene(2);
    }
}
