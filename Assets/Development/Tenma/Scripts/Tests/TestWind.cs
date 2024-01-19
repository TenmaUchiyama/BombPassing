using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestWind : MonoBehaviour
{

    [Range(0, 360)]
    [SerializeField] private float angle;
    [SerializeField] private Vector2 angleVector;
    [SerializeField] private RectTransform rect; 
    [SerializeField] private TextMeshProUGUI debugText; 
    [SerializeField] private Transform _windTransform;
    
    
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
               ChangeDir();
            }
            DisplayImage();
        }
        else
        {
            DisplayText();
        }
    }


    private void ChangeDir() 
    {
            int windInd = Random.Range(0, windDir.Count);
                angleVector = new Vector2(windDir[windInd][0], windDir[windInd][1]);
                float angleRadians = Mathf.Atan2(angleVector.y, angleVector.x);
                float angleDegrees = Mathf.Rad2Deg * angleRadians;
                _windTransform.rotation = Quaternion.Euler(0, -angleDegrees, 0);
                rect.rotation = Quaternion.Euler(0, 0, angleDegrees);

              
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
