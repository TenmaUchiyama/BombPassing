using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestWind : MonoBehaviour
{


    [SerializeField] private Vector2 angleVector;
    [SerializeField] private RectTransform rect; 
    [SerializeField] private TextMeshProUGUI debugText; 
    
    
    private List<int[]> windDir = new List<int[]>
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



    public void GenerateWind()
    {
        if (IsGenerateThisTime())
        {
            if(IsChanginDirection())
            {
                int windInd = Random.Range(0, windDir.Count);
                angleVector = new Vector2(windDir[windInd][0], windDir[windInd][1]);
                float angleRadians = Mathf.Atan2(angleVector.y, angleVector.x);
                float angleDegrees = Mathf.Rad2Deg * angleRadians;
                rect.rotation = Quaternion.Euler(0, 0, angleDegrees);
                Debug.Log("change dir");
            }
            else
            {
                Debug.Log("not change dir");
            }
            DisplayImage();
        }
        else
        {
            DisplayText();
        }
    }



    public bool IsGenerateThisTime()
    {
        float weightedRange = Random.Range(0f, 1f);
        return weightedRange >= 0.5f; 
    }


    public bool IsChanginDirection()
    {
        float weightedRange = Random.Range(0f, 1f);
        return weightedRange >= 0.4f; 
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    


    private void DisplayImage()
    {
        rect.gameObject.SetActive(true);
        debugText.gameObject.SetActive(false);
    }

    private void DisplayText()
    {
        rect.gameObject.SetActive(false);
        debugText.gameObject.SetActive(true);
    }
    
}
