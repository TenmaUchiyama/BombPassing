using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class Bomb : MonoBehaviour
{
    const float GRAVITY = 9.81f; //重力加速度を定義します。
    public float gravityScale = 1.0f;//重力の適用具合を定義します。


    private void Start() {
        GameManager.Instance.onGameOverEvent += onGameOver;
    }

    void Update()
    {

        Vector3 getInputMoveDir = GameInput.Instance.GetUserMoveDirNormalized();
       
        this.transform.Translate(getInputMoveDir *gravityScale * Time.deltaTime);
        const newGravity = GRAVITY * getInputMoveDir  * gravityScale * Vector3.down; 
        
        Physics.gravity = new Vector3(0, GRAVITY, 0);

        if(this.transform.position.y < 1.0f) FallFromPlane();
    }

    private void onGameOver(object sender, EventArgs e)
    {
        Debug.Log("GameOver from ball");
      this.Destroy();
    }

 


    
    private void FallFromPlane() 
    { 
        GameManager.Instance.OnGameOver();
    }

    


    private void Destroy() 
    {
        Destroy(this);
    }

    
}