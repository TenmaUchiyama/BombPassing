using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DeathTrigger : MonoBehaviour
{
    public GameObject DeathMenu;
    private IEnumerator coroutine;
    public GameObject explosion;
    public Boolean loseCheck = true;
    // Start is called before the first frame update

    private void Awake()
    {
        NewGameManager.OnGameStateChanged += GameManagerOnGameStateChange;
    }

    private void OnDestroy()
    {
        NewGameManager.OnGameStateChanged -= GameManagerOnGameStateChange;
    }

    private void GameManagerOnGameStateChange(NewGameManager.GameState state)
    {
        if (state == NewGameManager.GameState.GameLose)
        {
            coroutine = WaitForTrigger(2.0f);
            StartCoroutine(coroutine);
        }
    }
    void Start()
    {
        DeathMenu.SetActive(false);
        /*coroutine = WaitForTrigger(2.0f);
        StartCoroutine(coroutine);
        explosion.SetActive(false);*/
    }

        // Update is called once per frame
        void Update()
        {
        
        }

    private IEnumerator WaitForTrigger(float waitTime)
    {
        //if (loseCheck == true)
        //{
            yield return new WaitForSeconds(waitTime);
            explosion.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            explosion.SetActive(false);
            DeathMenu.SetActive(true);
            //loseCheck = false;
        //}
    }
}
