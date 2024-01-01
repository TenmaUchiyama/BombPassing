using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class WindSystem : Singleton<WindSystem>
{
    

  
    
    

    [SerializeField] private int startWindCount = 4;

    [SerializeField] private Image windIndicator;
    [SerializeField] private TextMeshProUGUI windForceIndicator; 

    private Rigidbody windedObject;
    private Vector3 windDirVec; 
    
    private bool isWind = false;
    private int windDirInd = 0;
    
    
    private float[] windForce = new float[]
    {
        0.6f,
        1.2f,
        2f
    };

    private int windForceInd = 0; 
    

    
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
        
        
      ChangeWindDir();
    }


    public void SetWindedObject(Rigidbody setWindedObject)
    {
        
        windedObject = setWindedObject;
    }

    private void FixedUpdate()
    {
        
        if (!windedObject) return;
        if (!isWind) return;

       
        // Rigidbodyに風の力を適用
        windedObject.AddForce(windDirVec * windForce[windForceInd]);
    }

    private void OnPassCountChanged(object sender, EventArgs e)
    {
        Debug.Log(windForce[windForceInd].ToString());
        
        int passCount = GameManager.Instance.PassCount;
      
        if (passCount < startWindCount) return;
    
        isWind = IsGenerateThisTime();

        
        windIndicator.gameObject.SetActive(isWind);
        if (!isWind) return;
        windForceIndicator.text = windForce[windForceInd].ToString();

        if (!IsChangingDirection()) return;

        ChangeWindDir();


        int forceAvailable = 1;

        if (passCount > 15 && passCount <= 20)
        {
            forceAvailable = 2;
        }else if(passCount > 20)
        {
            forceAvailable = 3; 
        }
        
        windForceInd = Random.Range(0, forceAvailable);
        

    }


    // private void Update()
    // {
    //     Debug.Log($"<color=red>x: {windDir[windDirInd][0]} z: {windDir[windDirInd][1]}</color>");
    // }


    private void ChangeWindDir()
    {
        windDirInd = Random.Range(0, windDir.Count);
        windDirVec =  new Vector3(windDir[windDirInd][0], 0f, windDir[windDirInd][1]);
        Vector2 angleVector = new Vector2(windDir[windDirInd][0], windDir[windDirInd][1]);
        float angleRadians = Mathf.Atan2(angleVector.y, angleVector.x);
        float angleDegrees = Mathf.Rad2Deg * angleRadians + 90;
        windIndicator.transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
        Debug.Log("change dir");
    }


    private bool IsGenerateThisTime()
    {
        float weightedRange = Random.Range(0f, 1f);
        
        return weightedRange >= 0.5f; 
    }


    private bool IsChangingDirection()
    {
        float weightedRange = Random.Range(0f, 1f);
        
        return weightedRange >= 0.4f; 
    }
    
    
    private bool IsChangeWindForce()
    {
        float weightedRange = Random.Range(0f, 1f);
        
        return weightedRange >= 0.8f; 
    }
    
}
