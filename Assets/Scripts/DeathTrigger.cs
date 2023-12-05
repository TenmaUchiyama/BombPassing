using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private float time = 5.0f;
    public GameObject DeathMenu;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        DeathMenu.SetActive(false);
        coroutine = WaitForTrigger(2.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitForTrigger(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
            DeathMenu.SetActive(true);
        }
    }
}
