using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject standardBall;
    private GameObject bomb; 
    void Start()
    {
        GameManager.Instance.OnGameModeChanged += onGameModeChanged;
        
    }



    private void onGameModeChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsPlayMode())
        {
            bomb = Instantiate(standardBall, this.transform);
            bomb.transform.position = this.transform.position;
        }
    }
    
    
    
    // Update is called once per frame
//     void FixedUpdate()
//     {
//         // if (!bomb) return;
//         // this.transform.position = new Vector3(bomb.transform.position.x, 1.1f, bomb.transform.position.z);
//     }
}
