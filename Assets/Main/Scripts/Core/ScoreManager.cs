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
    
    

    private int _rank;

    public int Rank => _rank = -1 ;
    
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

            rankText.text += $"{i + 1}   <size=50>{scoreDataList[i].score}</size>     {scoreDataList[i].savedDate}\n";
            if (scoreDataList[i].score == newScoreData.score &&
                scoreDataList[i].savedDate == newScoreData.savedDate)
            {
                _rank = i + 1;
            }
        }




       //display top three
       firstText.text = $"1st: <size=55>{scoreDataList[0].score}</size>  {scoreDataList[0].savedDate}";
       firstText.color = new Color(255, 215, 0, 1);
       if (scoreDataList.Count < 1) return; 
       
       secondText.text = $"2nd: <size=55>{scoreDataList[1].score}</size>   {scoreDataList[1].savedDate}";
        secondText.color = new Color32(162, 162, 162, 255);
        if (scoreDataList.Count < 2) return; 
       
       thirdText.text = $"3rd: <size=55>{scoreDataList[2].score}</size>  {scoreDataList[2].savedDate}";
       thirdText.color = new Color32(205, 127, 50, 255);
       string rankDislay = _rank.ToString();
       if (_rank == 0) rankDislay = "Out of Rank";
       fourthText.text = $"Your Rank:   {rankDislay}   <size=55>{newScoreData.score}</size>  {newScoreData.savedDate}";
        
       
       



    }


   
    public void ClearData()
    {
        _savingSystem.ClearData();
    }
}
