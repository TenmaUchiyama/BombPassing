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

    
            if (scoreDataList[i].score == newScoreData.score &&
                scoreDataList[i].savedDate == newScoreData.savedDate)
            {
                _rank = i + 1;
                break;
            }
        }




       //display top three
       firstText.text = $"1st: {scoreDataList[0].score}  {scoreDataList[0].savedDate}";

       if (scoreDataList.Count < 1) return; 
       
       secondText.text = $"2nd: {scoreDataList[1].score}  {scoreDataList[1].savedDate}";
        
       if (scoreDataList.Count < 2) return; 
       
       thirdText.text = $"3rd: {scoreDataList[2].score}  {scoreDataList[2].savedDate}";
       
       fourthText.text = $"Your Score: {newScoreData.score}  {newScoreData.savedDate}";




    }


   
    public void ClearData()
    {
        _savingSystem.ClearData();
    }
}
