using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    
    private SavingSystem _savingSystem = new SavingSystem();

    [SerializeField] private TextMeshProUGUI _debugText;

    private int _rank;

    public int Rank => _rank = -1 ;
    
    public void Start()
    {
        int scoreData = DataHolder.PassCountHolder; 
     
        ScoreData newScoreData = new ScoreData()
        {
            score = scoreData, savedDate = DateTime.Now.ToString()
        }; 
        
        //作成したスコアを過去データに入れる。
        ScoreDataList scoreDataList = _savingSystem.UpdateScoreData(newScoreData); 
       
    
        //比較してランクを調べる
        for (int i = 0; i < scoreDataList.scoredataList.Count; i++)
        {

    
            if (scoreDataList.scoredataList[i].score == newScoreData.score &&
                scoreDataList.scoredataList[i].savedDate == newScoreData.savedDate)
            {
                _rank = i + 1;
                break;
            }
        }


        for (int i = 0; i < scoreDataList.scoredataList.Count; i++)
        {
            Debug.Log ($"{i + 1}. {scoreDataList.scoredataList[i].score} {scoreDataList.scoredataList[i].savedDate} \n");
            _debugText.text +=
                $"{i + 1}. {scoreDataList.scoredataList[i].score} {scoreDataList.scoredataList[i].savedDate} \n"; 
        }

        _debugText.text += "\n\n\n";
        _debugText.text += "Your Result: \n"; 
        _debugText.text +=  $"{newScoreData.score} {newScoreData.savedDate} \n"; 
        
        
        
        
    }



    public void ClearData()
    {
        _savingSystem.ClearData();
    }
}
