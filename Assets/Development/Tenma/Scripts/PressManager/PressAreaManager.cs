


using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class PressAreaManager : MonoBehaviour
{


    [SerializeField] Canvas screenCanvas;
    [SerializeField] private TextMeshProUGUI instructionTxt;
    
    [SerializeField] private RectTransform pressAreaOnePrefab;
    [SerializeField] private RectTransform pressAreaTwoPrefab;


    private PressArea _pressAreaOne; 
    private PressArea _pressAreaTwo;
    

    private PressAreaType _formerArea = PressAreaType.ONE;
    


     
     





    private float hOffset = 150f; 
    private float wOffset = 150f;
    
    private bool isAlreadyInitialized = false;
    private bool isAlreadySwapped = false;
    private bool isPlaying = true;
    
    void Start() {
        InstantiateArea(PressAreaType.ONE);
        _formerArea = PressAreaType.ONE;


        GameManager.Instance.OnGameModeChanged += onGameModeChanged;
    }


    private void onGameModeChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsInitMode())
        {
            isAlreadyInitialized = false;
        }

        if (GameManager.Instance.IsPlayMode())
        {
            isAlreadyInitialized = true;
            InstantiateArea(PressAreaType.TWO);
        }


        if (GameManager.Instance.IsGameOverMode())
        {
            Destroy(_pressAreaOne.gameObject);
            Destroy(_pressAreaTwo.gameObject);
        }
    }
    private void StartGame()
    {
        InstantiateArea(PressAreaType.TWO);
        Debug.Log("GameStart!");
        _formerArea = PressAreaType.TWO;
    }


    private void Update()
    {

        if (!isAlreadyInitialized)
        {

            if (_pressAreaOne.IsAreaPressed && GameManager.Instance.IsInitMode())
            {
               GameManager.Instance.SetReadyMode(true);
            }

            if (!_pressAreaOne.IsAreaPressed && GameManager.Instance.IsReady())
            {
                GameManager.Instance.SetReadyMode(false);
            }

            return;
        }
        
        
        
        if (!_pressAreaOne|| !_pressAreaTwo) return;
        if (!_pressAreaOne.IsAreaPressed && !_pressAreaTwo.IsAreaPressed)
        {
            Debug.Log("<color=green>UPPPPPPPP</color>");
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
                instructionTxt.color = Color.gray;
                instructionTxt.text = "Move Gray!";
                break;
            case PressAreaType.TWO:
                randomPosition = _pressAreaOne == null
                    ? GetValidRandomPosition()
                    : GetValidRandomPosition(_pressAreaOne.GetRectTransform());
                _pressAreaTwo.GetRectTransform().position = randomPosition; 
                _formerArea = PressAreaType.ONE;
             
                instructionTxt.color = Color.red;
                instructionTxt.text = "Press Red!";
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
                tempArea = Instantiate(pressAreaOnePrefab, screenCanvas.transform);
                randomPosition = _pressAreaTwo == null
                    ? GetValidRandomPosition()
                    : GetValidRandomPosition(_pressAreaTwo.GetRectTransform());
                tempArea.position = randomPosition;
                _pressAreaOne = tempArea.GetComponent<PressArea>();
                _formerArea = PressAreaType.ONE;
                break;
            case PressAreaType.TWO:
                tempArea = Instantiate(pressAreaTwoPrefab, screenCanvas.transform);
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