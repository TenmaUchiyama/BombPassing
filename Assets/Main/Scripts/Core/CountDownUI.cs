using System;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{

    private const string POPUP_TRIGGER = "popUpTrigger";


   [SerializeField] TextMeshProUGUI countDownText; 
   [SerializeField] AudioClip croppedClipCount;

   [SerializeField] AudioClip croppedClipStart;
   [SerializeField] private GameObject textVisual; 


    private Animator animator;
    private AudioSource audioSource;
    private bool isCountDownStarted = false;

     private float initialReadyCount = 3f;
    private float readyCount = 0;
    private int previousReadyCount; 



    private void Start() {
        
        animator= GetComponent<Animator>();
            readyCount = initialReadyCount;
        GameManager.Instance.OnGameModeChanged += OnGameModeChanged;


        audioSource = GetComponent<AudioSource>();
        countDownText.text = "Hold Button To Start";
    }

    private void  OnGameModeChanged(object sender, EventArgs e )
    {
        
        
  
        if(GameManager.Instance.IsReady())
        {
         
            isCountDownStarted = true;

           
        }else{
            countDownText.text = "Hold Button To Start";
            Clear();
            isCountDownStarted = false;
        }


        if (GameManager.Instance.IsGameOverMode())
        {
            textVisual.SetActive(false);
        }
    }

    
    private void Update() {
        if(!isCountDownStarted) return;
         readyCount -= Time.deltaTime; 
            int readyCountAsInt = Mathf.CeilToInt(readyCount);
            if (readyCountAsInt < 0)
                {
                    GameManager.Instance.SetPlayMode(this);
                    isCountDownStarted = false;
                    return;
                } 

            if(readyCountAsInt != previousReadyCount)
            {
             
                previousReadyCount = readyCountAsInt; 
                string popUpText = readyCountAsInt == 0 ? "Start" : readyCountAsInt.ToString();
                PopUpLabel(popUpText);
            }


               

          
    }

  

    private void Clear() 
    {
        countDownText.text = "";
        readyCount = initialReadyCount; 
        previousReadyCount = 0;
    }


    private void PopUpLabel(string label)
    {

        
        Debug.Log("<color=blue>よばれてる</color>");

        countDownText.text = label; 
        animator.SetTrigger(POPUP_TRIGGER); 
        
        if(Mathf.CeilToInt(readyCount) == 0 )
        {
            audioSource.clip = croppedClipStart;
           
        }
        else
        {
            audioSource.clip = croppedClipCount;

        }
          
   
        audioSource.Play();
   
        

        
    }





 

}
