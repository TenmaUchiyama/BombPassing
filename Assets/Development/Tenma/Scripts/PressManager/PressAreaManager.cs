


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
    

     private List<RectTransform> pressAreas = new List<RectTransform>();







    private float hOffset = 150f; 
    private float wOffset = 150f;
    private bool isClicked = false; 


    private void Awake() {
        
    }
    void Start() {
    DisplayAreas(out _pressAreaOne);
    }


    private void Update()
    {

        
        if (!_pressAreaOne|| !_pressAreaTwo) return;

        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_formerArea == FormerArea.ONE)
            {
                Destroy(_pressAreaOne.gameObject);
                DisplayAreas(out _pressAreaOne);
                _formerArea = FormerArea.TWO;
                Debug.Log("Called1");
            }else
            {
                Destroy(_pressAreaTwo.gameObject);
                DisplayAreas(out _pressAreaTwo);
                _formerArea = FormerArea.ONE;
                Debug.Log("Called2");
            }
        }
       
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


    private Vector2 RandomPositionTest()
    {
        float randY = Random.Range(hOffset, Screen.height - hOffset);
        float randX = Random.Range(wOffset, Screen.width - wOffset);
        return new Vector2(randX, randY); 
    }

    private void DisplayPressArea() 
    {

       
        Debug.Log("Called");
        Vector2 randomPosition; 
        if(pressAreas.Count < 1)
        {
            randomPosition = GetValidRandomPosition();
        }else{
            randomPosition = GetValidRandomPosition(pressAreas[1]);
        }
        RectTransform newArea = Instantiate(pressAreaPrefab,screenCanvas.transform);
        newArea.position = randomPosition;

        pressAreas.Add(newArea);


        if(pressAreas.Count > 2)
        {
           
            RectTransform prevArea = pressAreas[0]; 
            pressAreas.RemoveAt(0); 
            Destroy(prevArea.gameObject);
            
        }


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