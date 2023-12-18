using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public enum PressAreaType
{
    ONE,  
    TWO
}

public class PressArea : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler,IPointerMoveHandler
{
    // Start is called before the first frame update
    private RectTransform _pressArea;

    private bool _isAreaPressed = false;
    private bool _isPrevAreaPressed;
    private bool _isPressedOnce = false;
    public bool IsAreaPressed => _isAreaPressed;



    public event EventHandler OnAreaPressedChanged; 
    void Start()
    {
        _isPrevAreaPressed = _isAreaPressed;
        this._pressArea = GetComponent<RectTransform>(); 
    }


    private void Update() {
        if(_isPrevAreaPressed != _isAreaPressed)
        {
             if(OnAreaPressedChanged != null)OnAreaPressedChanged.Invoke(this, EventArgs.Empty);
            _isPrevAreaPressed = _isAreaPressed; 
        }
    }


    
    
    
    
    public void OnPointerDown(PointerEventData eventData)
    {

        _isPressedOnce = true; 
        Vector2 touchPosition = eventData.position;
        float distance = Vector2.Distance(touchPosition, this._pressArea.position);
    
        _isAreaPressed = distance <= 150.0f;
    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isAreaPressed = false;
        _isPressedOnce = false;
    }
  
    public void OnPointerMove(PointerEventData eventData)
    {

        if (!_isPressedOnce) return; 
        Vector2 touchPosition = eventData.position;
        
        float distance = Vector2.Distance(touchPosition, this._pressArea.position);
    
        _isAreaPressed = distance <= 150.0f;
    }

    public RectTransform GetRectTransform()
    {
        return this._pressArea; 
    }
}
