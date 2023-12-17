using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleroMeterTest : MonoBehaviour
{

    [SerializeField] private Transform testerObject;

    [SerializeField] private float scale = 1f;

    [SerializeField] private float damperRatio = 0.5f;

    
    private void Update()
    {
        float physicsLerp = Mathf.Lerp(0, Input.acceleration.x * scale, damperRatio);
        testerObject.transform.position = new Vector3(physicsLerp, 0, 0); 
    }
}
