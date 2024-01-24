using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PAMTemp : MonoBehaviour
{
    

    [SerializeField] Canvas screenCanvas;
    [SerializeField] private RectTransform pressAreaOne;
    [SerializeField] private RectTransform pressAreaTwo;


    private PressArea _pressAreaOne; 
    private PressArea _pressAreaTwo;


    private PressAreaType _formerArea = PressAreaType.ONE;
    


     
     





    private float hOffset = 150f; 
    private float wOffset = 150f;
    
    private bool isAlreadInitialized = false;
    private bool isAlreadySwapped = false;


    private void Awake() {
        
    }
    
    
    
    void Start()
    {
        InstantiateArea(PressAreaType.ONE);
    }


    private void StartGame()
    {
        

        InstantiateArea(PressAreaType.TWO);
        _formerArea = PressAreaType.TWO;
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
           
            return;
        }
        

        if (_formerArea == PressAreaType.ONE)
        {
            if (!_pressAreaOne.IsAreaPressed)
            {
              MoveArea(PressAreaType.ONE);
            }
        }
        else
        {
            if (!_pressAreaTwo.IsAreaPressed)
            {
                MoveArea(PressAreaType.TWO);
            }
           
        }
       
    }

    private void SwapOne()
    {
        Destroy(_pressAreaOne.gameObject);
        DisplayAreas(out _pressAreaOne);
        _formerArea = PressAreaType.TWO;
    }


    private void SwapTwo()
    {
        Destroy(_pressAreaTwo.gameObject);
        DisplayAreas(out _pressAreaTwo);
        _formerArea = PressAreaType.ONE;
    }

    private void MoveArea(PressAreaType type)
    {
        
        Vector2 randomPosition;
        switch (type)
        {
            case PressAreaType.ONE:
                randomPosition = _pressAreaTwo == null
                    ? GetValidRandomPosition()
                    : GetValidRandomPosition(_pressAreaTwo.GetRectTransform());
                _pressAreaOne.GetRectTransform().position = randomPosition;
                _formerArea = PressAreaType.TWO;
                break;
            case PressAreaType.TWO:
                randomPosition = _pressAreaOne == null
                    ? GetValidRandomPosition()
                    : GetValidRandomPosition(_pressAreaOne.GetRectTransform());
                _pressAreaTwo.GetRectTransform().position = randomPosition; 
                _formerArea = PressAreaType.ONE;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }




    private void InstantiateArea(PressAreaType type)
    {
        RectTransform tempArea;
        Vector2 randomPosition;
        switch (type)
        {
            case PressAreaType.ONE:
                 tempArea = Instantiate(pressAreaOne, screenCanvas.transform);
                 randomPosition = _pressAreaTwo == null
                    ? GetValidRandomPosition()
                    : GetValidRandomPosition(_pressAreaTwo.GetRectTransform());
                tempArea.position = randomPosition;
                _pressAreaOne = tempArea.GetComponent<PressArea>();
                _formerArea = PressAreaType.ONE;
                break;
            case PressAreaType.TWO:
                 tempArea = Instantiate(pressAreaTwo, screenCanvas.transform);
                 randomPosition = _pressAreaOne== null
                    ? GetValidRandomPosition()
                    : GetValidRandomPosition(_pressAreaOne.GetRectTransform());
                tempArea.position = randomPosition;
                _pressAreaTwo = tempArea.GetComponent<PressArea>();
                _formerArea = PressAreaType.TWO;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }



    // ReSharper disable Unity.PerformanceAnalysis
    private void DisplayAreas(out PressArea pressArea)
    {
        RectTransform tempArea = Instantiate(pressAreaOne, screenCanvas.transform);
        if(_formerArea == PressAreaType.ONE )tempArea.GetComponent<Image>().color = Color.red;
        PressArea prevArea = _formerArea == PressAreaType.ONE ? _pressAreaTwo : _pressAreaOne;
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
