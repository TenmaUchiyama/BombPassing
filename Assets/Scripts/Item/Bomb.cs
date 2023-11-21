using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BombPassing.Core;
using UnityEngine.EventSystems;
using System;


namespace BombPassing.Item 
{

public class Bomb : MonoBehaviour
{
    const float Gravity = 9.81f; //重力加速度を定義します。
    public float gravityScale = 1.0f;//重力の適用具合を定義します。



    void Update()
    {
        Vector3 getInputMoveDir = GameInput.Instance.GetUserMoveDirNormalized();
  
        this.transform.Translate(getInputMoveDir *gravityScale * Time.deltaTime);
        // Physics.gravity = Gravity * getInputMoveDir  * gravityScale ; 
        // GameManager.Instance.onGameOverEvent += onGameOver;

        if(this.transform.position.y < -1.0f) FallFromPlane();
    }



    
    private void FallFromPlane() 
    { 
        GameManager.Instance.OnGameOver(this);
          Destroy(gameObject);
    }

    



    
}
}