using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class GameInput : Singleton<GameInput>
{



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

        vector.x = -Input.acceleration.x; 
        vector.z = -Input.acceleration.y; 
        vector.y = 1;

        if(vector.sqrMagnitude > 1) vector.Normalize(); 
 
        return vector;
    }


    public void GetPressedItem() 
    {
        if(Input.GetMouseButtonDown(0))
        {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       RaycastHit hit; 
       
       if(Physics.Raycast(ray, out hit))
       {
        Debug.Log(hit.transform.name);
       }}
    }
}
