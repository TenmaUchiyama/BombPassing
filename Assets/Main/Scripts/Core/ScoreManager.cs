// using System;

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    
    private SavingSystem _savingSystem = new SavingSystem();

    private float fadeDuration = 0.8f; 

    private Image flashingImg; 
    private TextMeshProUGUI flashingTxt;




    [SerializeField] private TextMeshProUGUI firstText;
    [SerializeField] private TextMeshProUGUI secondText;
    [SerializeField] private TextMeshProUGUI thirdText;
    [SerializeField] private TextMeshProUGUI fourthText;
    
    [SerializeField] private TextMeshProUGUI rankText; 

    [SerializeField] private GameObject _goldHat; 
    [SerializeField] private GameObject _silverHat; 
    [SerializeField] private GameObject _bronzeHat; 
    [SerializeField] private GameObject _yourRankHat; 

    
    

    private int _rank;

    public int Rank => _rank = 0;

    
    
    public void Start()
    {
        
        int scoreData = DataHolder.PassCountHolder; 
     
        ScoreData newScoreData = new ScoreData()
        {
            score = scoreData, savedDate = DateTime.Now.ToString()
        }; 
        
        //Insert the current data to past data
        ScoreDataList scoreDataListObject = _savingSystem.UpdateScoreData(newScoreData);

        List<ScoreData> scoreDataList = scoreDataListObject.scoredataList; 
        
        
        
        
        
        //Compare and search the rank
        for (int i = 0; i < scoreDataList.Count; i++)
        {

            rankText.text += $"{i + 1}   {scoreDataList[i].score}    {scoreDataList[i].savedDate} \n";
            if (scoreDataList[i].score == newScoreData.score &&
                scoreDataList[i].savedDate == newScoreData.savedDate)
            {
                _rank = i + 1;
            }
        }




       //display top three
       _goldHat.SetActive(true);
       firstText.text = $"1st: <size=80>{scoreDataList[0].score}</size>  <size=50>{scoreDataList[0].savedDate}</size>";
       // firstText.color = new Color(255, 215, 0, 1);
       if (scoreDataList.Count < 1) return; 
       
       _silverHat.SetActive(true);
       secondText.text = $"2nd: <size=75>{scoreDataList[1].score}</size>   <size=50>{scoreDataList[1].savedDate}</size>";
        // secondText.color = new Color32(162, 162, 162, 255);
        if (scoreDataList.Count < 2) return; 
       
       _bronzeHat.SetActive(true);
       thirdText.text = $"3rd: <size=75>{scoreDataList[2].score}</size>  <size=50>{scoreDataList[2].savedDate}</size>";
       // thirdText.color = new Color32(205, 127, 50, 255);
       string rankDislay = _rank.ToString();


       if (_rank == 0) rankDislay = "Out of Rank";

       
       switch(_rank)
       {
        case 1:
            flashingTxt = firstText;
             flashingImg = _goldHat.GetComponent<Image>(); 
        break; 
            
        case 2: 
           flashingTxt = secondText; 
            flashingImg = _silverHat.GetComponent<Image>(); 
        break; 

        case 3: 
           flashingTxt = thirdText; 
           flashingImg = _bronzeHat.GetComponent<Image>(); 
        break; 
        
       }


       if(_rank <= 3) return;
       
       _yourRankHat.SetActive(true);
       fourthText.text = $"Your Rank:   <size=50>{rankDislay}</size>   <size=75>{newScoreData.score}</size>  <size=50>{newScoreData.savedDate}</size>";
        
       
       



    }



   private IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeText(0f, fadeDuration)); // Alphaを0にする
            yield return new WaitForSeconds(0.1f); // 少し待機
            yield return StartCoroutine(FadeText(1f, fadeDuration)); // Alphaを1に戻す
            yield return new WaitForSeconds(0.1f); // 少し待機
        }
    }

    private IEnumerator FadeText(float targetAlpha, float duration)
    {
        float startAlpha = flashingTxt.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            Color newColor = flashingTxt.color;
            newColor.a = newAlpha;
            flashingTxt.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最終的に確実にtargetAlphaに設定
        Color finalColor = flashingTxt.color;
        finalColor.a = targetAlpha;
        flashingTxt.color = finalColor;
    }
     



   
    public void ClearData()
    {
        _savingSystem.ClearData();
    }
}
