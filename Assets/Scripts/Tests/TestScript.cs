using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScript : MonoBehaviour
{ 

  [SerializeField] TextMeshProUGUI meter; 
  [SerializeField] RectTransform first; 
  [SerializeField] RectTransform second; 


  private void Update() {
    
    float distance = Vector2.Distance(first.position, second.position);

    meter.text = distance.ToString();

  }

    private bool IsTheNewPositionInside(Vector2 firstArea, Vector2 secondArea)
    {

        
        float distance = Vector2.Distance(firstArea, secondArea);
        Debug.Log(distance);
        return distance < 300;
    }




  void GetRandom() 
  {

    float hOffset = 150f; 
    float wOffset = 150f; 


    float randY = Random.Range(hOffset, Screen.height - hOffset);
    float randX = Random.Range(wOffset, Screen.width - wOffset);

    Vector2 randomPosition = new Vector2(randX, randY);
    first.position = randomPosition; 
    if (!IsTheNewPositionInside(randomPosition, second.position))
    {
    
      return;
        
    }

}

}