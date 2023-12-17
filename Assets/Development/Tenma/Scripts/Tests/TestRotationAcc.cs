using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestRotationAcc : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 0.5f;
    private Quaternion attitude;
    private void Start()
    {
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        Quaternion attitude;
        attitude = Input.gyro.attitude;
        attitude = new Quaternion(attitude.y, attitude.x, -attitude.z, attitude.w);
        Quaternion sub = Quaternion.Euler(-90, 0, 0);
        Quaternion targetRotation = sub * attitude;


        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);

    }

}
