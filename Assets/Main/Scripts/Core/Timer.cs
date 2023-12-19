using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    [SerializeField] Image timerImageContent; 
    [SerializeField] Image timerImageOutline; 


    [SerializeField] float pernalizeWeight = 1.5f;
    [SerializeField] float addTimeWeight = 200f;
    [SerializeField] float _maxGameTimer = 15f;



    private string DebugLogColor = "#87CEEB";

    private enum TimerArea 
    {
        GREEN, 
        YELLOW, 
        RED,
        DEAD
    }

    private TimerArea _currentTimerArea = TimerArea.GREEN; 
    private TimerArea _previousTimerArea; 
    private float _currentGameTimer;
    // Start is called before the first frame update

    private bool isColorChanged = false;



    private bool isPlayMode = false; 


    void Start()
    {
        _currentGameTimer = _maxGameTimer; 
        _previousTimerArea = _currentTimerArea; 
            Debug.Log($"CurrentTimer: {_currentGameTimer}, Max: {_maxGameTimer}");

        GameManager.Instance.OnGameModeChanged += onGameModeChanged;
    }


    private void onGameModeChanged(object sender, EventArgs e)
    {


    
        if(GameManager.Instance.IsPlayMode())
        {
            Debug.Log("It's Play Mode");
            isPlayMode = true;
        }else{
            isPlayMode = false;
        }


    }

    // Update is called once per frame
    void Update()
    {


        if(!isPlayMode) return;

        _currentGameTimer -= Time.deltaTime; 


        float timerRate =  _currentGameTimer / _maxGameTimer;
      

        timerImageContent.fillAmount = timerRate;


        if(timerRate > 0.6)
        {
            _currentTimerArea = TimerArea.GREEN;
        }
        else if(timerRate <= 0.6 && timerRate > 0.2)
        {
          _currentTimerArea = TimerArea.YELLOW; 
        }else if(timerRate <= 0.6 && timerRate > 0.2){
          _currentTimerArea = TimerArea.RED; 
        }else if(timerRate < 0.0f)
        {
            _currentTimerArea = TimerArea.DEAD;
        }



        if(_previousTimerArea != _currentTimerArea)
        {

            Color outlineColor = Color.black;
            switch(_currentTimerArea)
            {
                case TimerArea.GREEN:
                    timerImageContent.color = Color.green;
                    ColorUtility.TryParseHtmlString("#127800", out outlineColor);
                break;
                case TimerArea.YELLOW: 
                  timerImageContent.color = Color.yellow;
                  ColorUtility.TryParseHtmlString("#ECB700", out outlineColor);
                break;
                case TimerArea.RED: 
                  timerImageContent.color = Color.red;
                  ColorUtility.TryParseHtmlString("#910700", out outlineColor);
                break;
                 case TimerArea.DEAD: 
                 GameManager.Instance.SetGameOverMode(this);
                break;
            }

            timerImageOutline.color = outlineColor; 

        }

        _previousTimerArea = _currentTimerArea; 
    }



    public void PenalizeTimer(object sender)
    {
        // Debug.Log($"<color={DebugLogColor}>Penalized By: {sender}</color>");
        _currentGameTimer -= Time.deltaTime * 2f;
    }


    public void AddTime(object sender) 
    {
      Debug.Log($"<color={DebugLogColor}>Added By: {sender}</color>");
      float newTime = _currentGameTimer + Time.deltaTime * addTimeWeight;
      
      _currentGameTimer = newTime > _maxGameTimer ?  _maxGameTimer : newTime;
    }


}
