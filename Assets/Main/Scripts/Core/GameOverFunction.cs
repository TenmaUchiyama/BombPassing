using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverFunction : MonoBehaviour
{
    [SerializeField] private DataHolder dataHolder;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI resultText; 
    
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
            resultText.text += GameManager.Instance.PassCount.ToString(); 
            dataHolder.SetPassCountData(GameManager.Instance.PassCount);
         
        }
    }


    private IEnumerator  DisplayGameOver()
    {
        yield return new WaitForSeconds(2);
        gameOverUI.SetActive(true);
    }
}
