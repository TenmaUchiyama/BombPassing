using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressArea : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler,IPointerMoveHandler
{
    // Start is called before the first frame update
    private RectTransform pressArea;

    private bool isAreaPressed = false;
    private bool isPressedOnce = false;

    public bool IsAreaPressed => isAreaPressed;
    void Start()
    {
        this.pressArea = GetComponent<RectTransform>(); 
    }


    private void Update()
    {
        
    }
    
    
    
    
    public void OnPointerDown(PointerEventData eventData)
    {

        isPressedOnce = true; 
        Vector2 touchPosition = eventData.position;
        float distance = Vector2.Distance(touchPosition, this.pressArea.position);
    
        isAreaPressed = distance <= 150.0f;
    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isAreaPressed = false;
        isPressedOnce = false;
    }
  
    public void OnPointerMove(PointerEventData eventData)
    {

        if (!isPressedOnce) return; 
        Vector2 touchPosition = eventData.position;
        
        float distance = Vector2.Distance(touchPosition, this.pressArea.position);
    
        isAreaPressed = distance <= 150.0f;
    }

    public RectTransform GetRectTransform()
    {
        return this.pressArea; 
    }
}
