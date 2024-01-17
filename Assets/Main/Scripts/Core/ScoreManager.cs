// using System;

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    
    private SavingSystem _savingSystem = new SavingSystem();

    [SerializeField] private TextMeshProUGUI firstText;
    [SerializeField] private TextMeshProUGUI secondText;
    [SerializeField] private TextMeshProUGUI thirdText;
    [SerializeField] private TextMeshProUGUI fourthText;
    
    [SerializeField] private TextMeshProUGUI rankText; 

    [SerializeField] private GameObject _goldHat; 
    [SerializeField] private GameObject _silverHat; 
    [SerializeField] private GameObject _bronzeHat; 

    
    

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
       if (_rank <= 3) return; 
       fourthText.text = $"Your Rank:   {rankDislay}   <size=50>{newScoreData.score}</size>  <size=50>{newScoreData.savedDate}</size>";
        
       
       



    }


   
    public void ClearData()
    {
        _savingSystem.ClearData();
    }
}
