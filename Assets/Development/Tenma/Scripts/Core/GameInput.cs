using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class GameInput : Singleton<GameInput>
{
    public static GameInput instance; 
    

    private UserInput userInputAction; 

 

    private void Start() {
        Input.gyro.enabled = true;
        userInputAction = new UserInput();
        userInputAction.Enable();
    }



    public Vector3 GetUserMoveDirNormalized() 
    {
        Vector2 inputDir = userInputAction.User.Move.ReadValue<Vector2>(); 
       
       Debug.Log(inputDir);

        Vector3 moveDir = new Vector3(-inputDir.x, 0, -inputDir.y);


        return moveDir.normalized;
    }

    public Vector3 GetDeviceGyroNormalized()
    {
         Vector3 vector = new Vector3();

        vector.x = -Input.gyro.rotationRateUnbiased.x;
        vector.z = -Input.gyro.rotationRateUnbiased.y;
        vector.y = Input.gyro.rotationRateUnbiased.z;
 

        return vector.normalized;
    }
}
