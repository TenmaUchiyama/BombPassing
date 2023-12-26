using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;


public class WindSystem : MonoBehaviour
{
    
    
    

    private Rigidbody ballRb;

    private bool isWind = false;
    private float windForce = 2f;

    private int windDirInd = 0; 
    
    
    private   List<int[]> windDir = new List<int[]>
    {
        new int[] {0, 1},
        new int[] {1, 1},
        new int[] {1, 0},
        new int[] {1, -1},
        new int[] {0, -1},
        new int[] {-1, -1},
        new int[] {-1, 0},
        new int[] {-1, 1}
    };
    private void Start()
    {
        GameManager.Instance.OnPassCountChanged += OnPassCountChanged;
    }

    private void FixedUpdate()
    {
        if (!isWind) return; 
        // Rigidbodyを取得
        Rigidbody rb = GetComponent<Rigidbody>();

        // 風の方向を設定（ここではX軸方向）
        Vector3 windDirection = new Vector3(windDir[windDirInd][0], 0f, windDir[windDirInd][1]);

        // Rigidbodyに風の力を適用
        rb.AddForce(windDirection * windForce);
        
    }

    private void OnPassCountChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.PassCount == 4)
        {
            isWind = true; 
        }
        windDirInd = Random.Range(0, 8);
        Debug.Log($"{windDir[windDirInd][0]}, {windDir[windDirInd][1]}");
    }
}
