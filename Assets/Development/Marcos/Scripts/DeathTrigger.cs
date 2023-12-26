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

  
    // void Start()
    // {
    //     DeathMenu.SetActive(false);
    //     /*coroutine = WaitForTrigger(2.0f);
    //     StartCoroutine(coroutine);
    //     explosion.SetActive(false);*/
    // }

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
