


using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;


public class PressAreaManager : MonoBehaviour
{


    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform pressAreaPrefab;
    

     private List<RectTransform> pressAreas = new List<RectTransform>();







    private float hOffset = 150f; 
    private float wOffset = 150f;
    private bool isClicked = false; 


    private void Awake() {
        
    }
    void Start() {

        this.UpdateAsObservable() 
        .Where(_ => Input.GetKeyDown(KeyCode.Space))
        .Subscribe(_=> {DisplayPressArea();})
        .AddTo(this);
    

    }


    private void DisplayPressArea() 
    {

       

        Vector2 randomPosition; 
        if(pressAreas.Count < 1)
        {
            randomPosition = GetValidRandomPosition();
        }else{
            randomPosition = GetValidRandomPosition(pressAreas[1]);
        }
        RectTransform newArea = Instantiate(pressAreaPrefab,canvas.transform);
        newArea.position = randomPosition;

        pressAreas.Add(newArea);


        if(pressAreas.Count > 2)
        {
           
            RectTransform prevArea = pressAreas[0]; 
            pressAreas.RemoveAt(0); 
            Destroy(prevArea.gameObject);
            
        }


    }


    private Vector2 MapScreenPositionToCanvas(Vector2 screenPosition)
    {
        return new Vector2(screenPosition.x - Screen.width/2 , screenPosition.y - Screen.height/2);
    }

    
    private bool IsTheNewPositionInside(Vector2 firstArea, Vector2 secondArea)
    {
        if (pressAreas.Count == 0) return false;
        
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
                if (!IsTheNewPositionInside(randomPosition, previousArea.anchoredPosition))
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