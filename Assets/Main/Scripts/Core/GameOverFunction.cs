using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverFunction : MonoBehaviour
{
    [SerializeField] private DataHolder dataHolder;
    [SerializeField] private GameObject explosionEffect;

    [SerializeField] private BombSceneManager sceneManager;
    private void Start()
    {
     
        GameManager.Instance.OnGameModeChanged  += OnGameModeChangedFunc;
    }

    private void OnGameModeChangedFunc(object sender, EventArgs e)
    {
      
        if (GameManager.Instance.IsGameOverMode())
        {
            
            explosionEffect.SetActive(true);
            StartCoroutine(DisplayGameOver());
            dataHolder.SetPassCountData(GameManager.Instance.PassCount);
         
        }
    }


    private IEnumerator  DisplayGameOver()
    {
        yield return new WaitForSeconds(2);
        sceneManager.LoadGameOver(); 
    }
}
