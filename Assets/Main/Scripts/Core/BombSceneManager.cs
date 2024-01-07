using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombSceneManager : MonoBehaviour
{
  
    


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
