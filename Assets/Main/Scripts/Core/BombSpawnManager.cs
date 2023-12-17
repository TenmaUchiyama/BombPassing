using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject standardBall;
    
    void Start()
    {
        GameManager.Instance.OnGameModeChanged += onGameModeChanged;
    }



    private void onGameModeChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsPlayMode())
        {
            GameObject bomb = Instantiate(standardBall, this.transform);
            bomb.transform.position = this.transform.position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
