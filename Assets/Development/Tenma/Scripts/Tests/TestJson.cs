using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TestJson : MonoBehaviour
{

    [SerializeField] private int testScore = 10; 
    [SerializeField] private TextMeshProUGUI debugText;

    private SavingSystem _savingSystem = new SavingSystem();

    

    // Start is called before the first frame update
    public void TestingJsonSave()
    {
        //新しいスコアのScoreDataを作成する
        ScoreData newScoreData = new ScoreData()
        {
            score = testScore, savedDate = DateTime.Now.ToString()
        }; 
        
        //作成したスコアを過去データに入れる。
        ScoreDataList scoreDataList = _savingSystem.UpdateScoreData(newScoreData); 
        
    
        //比較してランクを調べる
        int rank = -1;

        for (int i = 0; i < scoreDataList.scoredataList.Count; i++)
        {

            Debug.Log(scoreDataList.scoredataList[i].savedDate);
            if (scoreDataList.scoredataList[i].score == newScoreData.score &&
                scoreDataList.scoredataList[i].savedDate == newScoreData.savedDate)
            {
    
    
                rank = i + 1;
                break;
            }
        }
        
   
    }
    
    public void TestingJsonRead()
    {
        debugText.text = "";
        ScoreDataList result = _savingSystem.ReadData();
    

        for (int i = 0; i < result.scoredataList.Count; i++)
        {
            ScoreData sd = result.scoredataList[i]; 
            debugText.text += $"{i+1}.  {sd.score} {sd.savedDate} \n";
        }
       
    }

    public void TestJsonDelete()
    {
        _savingSystem.ClearData();
    }

}
