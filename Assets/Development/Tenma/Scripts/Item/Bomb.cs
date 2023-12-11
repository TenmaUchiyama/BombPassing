using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;




public class Bomb : MonoBehaviour
{
    const float Gravity = -9.81f; //重力加速度を定義します。
    public float gravityScale = 1.0f;//重力の適用具合を定義します。



    void Update()
    {
        Vector3 gameInputMoveDir = GameInput.Instance.GetDeviceGyroNormalized(); 
        Physics.gravity = Gravity * gameInputMoveDir  * gravityScale ;
        if(this.transform.position.y < -1.0f) FallFromPlane();
    }



    
    private void FallFromPlane() 
    { 
        GameManager.Instance.OnGameOver();
        Destroy(gameObject);
    }

    



    
}