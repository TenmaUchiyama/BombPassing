


using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class PressAreaManager : MonoBehaviour
{


    [SerializeField] Canvas screenCanvas;
    [SerializeField] RectTransform pressAreaPrefab;


    private PressArea _pressAreaOne; 
    private PressArea _pressAreaTwo;

    enum FormerArea
    {
        ONE, 
        TWO 
    }

    private FormerArea _formerArea = FormerArea.ONE;
    


     
     





    private float hOffset = 150f; 
    private float wOffset = 150f;
    
    private bool isAlreadInitialized = false;
    private bool isAlreadySwapped = false;


    private void Awake() {
        
    }
    
    
    
    void Start() {
    DisplayAreas(out _pressAreaOne);
    _formerArea = FormerArea.ONE;
    }


    private void StartGame()
    {
        Debug.Log("GameStart!");
        DisplayAreas(out _pressAreaTwo);
        _formerArea = FormerArea.TWO;
    }


    private void Update()
    {

        if (!isAlreadInitialized)
        {

            if (_pressAreaOne.IsAreaPressed)
            {
                StartGame();
                isAlreadInitialized = true;
            }
            return;
        }
        if (!_pressAreaOne|| !_pressAreaTwo) return;
        if (!_pressAreaOne.IsAreaPressed && !_pressAreaTwo.IsAreaPressed)
        {
            Debug.Log("<color=green>UPPPPPPPP</color>");
            return;
        }
        

        if (_formerArea == FormerArea.ONE)
        {
            if (!_pressAreaOne.IsAreaPressed)
            {
                SwapOne();
            }
        }
        else
        {
          
            
            if (!_pressAreaTwo.IsAreaPressed)
            {
                SwapTwo();
            }
           
        }
       
    }

    private void SwapOne()
    {
        Destroy(_pressAreaOne.gameObject);
        DisplayAreas(out _pressAreaOne);
        _formerArea = FormerArea.TWO;
    }


    private void SwapTwo()
    {
        Destroy(_pressAreaTwo.gameObject);
        DisplayAreas(out _pressAreaTwo);
        _formerArea = FormerArea.ONE;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void DisplayAreas(out PressArea pressArea)
    {
        
        
        RectTransform tempArea = Instantiate(pressAreaPrefab, screenCanvas.transform);
        if(_formerArea == FormerArea.ONE )tempArea.GetComponent<Image>().color = Color.red;
        PressArea prevArea = _formerArea == FormerArea.ONE ? _pressAreaTwo : _pressAreaOne;
        Vector2 randomPosition = Vector2.zero;
        
        if (prevArea)
        {
             randomPosition =GetValidRandomPosition(prevArea.GetRectTransform());
        }
        else
        {
              randomPosition = GetValidRandomPosition();
        }
        // Vector2 randomPosition = prevArea ? GetValidRandomPosition() : GetValidRandomPosition(prevArea.GetRectTransform());
        //
        tempArea.position = randomPosition; 
        pressArea = tempArea.GetComponent<PressArea>();
    }





    
    private bool IsTheNewPositionInside(Vector2 firstArea, Vector2 secondArea)
    {
        
        float distance = Vector2.Distance(firstArea, secondArea);
        
        Debug.Log("first: " + firstArea  + " second: " + secondArea + " distance: " +  distance + " width: " + Screen.width + " height: " + Screen.height);
        return distance < 300;
    }
    private Vector2 GetValidRandomPosition(RectTransform previousArea = null)
    {

        if(previousArea){
         for (int i = 0; i < 100; i++)
            {
                float randY = Random.Range(hOffset, Screen.height - hOffset);
                float randX = Random.Range(wOffset, Screen.width - wOffset);

                Vector2 randomPosition = new Vector2(randX, randY);
                if (!IsTheNewPositionInside(randomPosition, previousArea.transform.position))
                {
                    return randomPosition;
                }else{
                
                }
            }
        }else{
             float randY = Random.Range(hOffset, Screen.height - hOffset);
            float randX = Random.Range(wOffset, Screen.width - wOffset);
             Vector2 randomPosition = new Vector2(randX, randY);
             return randomPosition;
        }
        // If no valid position is found after 100 attempts, return Vector2.zero
        return Vector2.zero;


    }


}