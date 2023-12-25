using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;


    private void Start()
    {
        textMesh.text = "Total Count: " + DataHolder.PassCountHolder;  
    }
}
