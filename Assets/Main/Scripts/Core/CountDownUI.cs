using System;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{

    private const string POPUP_TRIGGER = "popUpTrigger";


   [SerializeField] TextMeshProUGUI countDownText; 

   [SerializeField] AudioClip croppedClipCount;
   [SerializeField] AudioClip croppedClipStart;
   


    private Animator animator; 

    private bool isCountDownStarted = false;

     private float initialReadyCount = 3f;
    private float readyCount = 0;
    private int previousReadyCount; 



    private void Start() {
        
        animator= GetComponent<Animator>();
            readyCount = initialReadyCount;
        GameManager.Instance.OnGameModeChanged += OnGameModeChanged;
        




    }

    private void  OnGameModeChanged(object sender, EventArgs e )
    {
        if(GameManager.Instance.IsReady())
        {
            isCountDownStarted = true; 
        }else{
            Clear();
           
            isCountDownStarted = false;
        }
    }

    
    private void Update() {
        if(!isCountDownStarted) return;
         readyCount -= Time.deltaTime; 
            int readyCountAsInt = Mathf.CeilToInt(readyCount);
            if (readyCountAsInt < 0)
                {
                    GameManager.Instance.SetPlayMode(this);
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


        countDownText.text = label; 
        animator.SetTrigger(POPUP_TRIGGER); 
        
        if(Mathf.CeilToInt(readyCount) == 0 ){
            AudioSource.PlayClipAtPoint(croppedClipStart, Camera.main.transform.position, 1.0f);
            return;
        }
          
   
             AudioSource.PlayClipAtPoint(croppedClipCount,Camera.main.transform.position, 1.0f); 
   


        
    }





 

}
