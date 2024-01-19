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

    [SerializeField] private AudioClip yellowAudioClip; 
    [SerializeField] private AudioClip redAudioClip; 
    



    private string DebugLogColor = "#87CEEB";


    private AudioSource _audioSource; 

    private enum TimerArea 
    {
        GREEN, 
        YELLOW, 
        RED,
        DEAD,
        NONE,
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
       
        _audioSource = GetComponent<AudioSource>();
        GameManager.Instance.OnGameModeChanged += onGameModeChanged;
    }


    private void onGameModeChanged(object sender, EventArgs e)
    {


    
        if(GameManager.Instance.IsPlayMode())
        {
           
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
        }else if(timerRate <= 0.2 && timerRate > 0.0){
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
                    _currentTimerArea = TimerArea.NONE;
                break;
                case TimerArea.NONE:
                    break; 
            }

            if (_currentTimerArea == TimerArea.RED)
            {

                _audioSource.Play();
            }
            else
            {
                if (_audioSource.isPlaying) _audioSource.Stop();
            }
            timerImageOutline.color = outlineColor; 

        }

        _previousTimerArea = _currentTimerArea; 
    }



    public void PenalizeTimer(object sender)
    {
      
        _currentGameTimer -= Time.deltaTime * 2f;
    }


    public void AddTime(object sender) 
    {
      
      float newTime = _currentGameTimer +  addTimeWeight;
      
      _currentGameTimer = newTime > _maxGameTimer ?  _maxGameTimer : newTime;
    }


}
