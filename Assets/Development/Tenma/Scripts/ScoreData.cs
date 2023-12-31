using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


[Serializable]
public class ScoreData
{
    public int score;
    public string savedDate;
}


[Serializable]
public class ScoreDataList
{
    public List<ScoreData> scoredataList = new List<ScoreData>();
  
}



public class SavingSystem
{


    private string savingFile = "/savedata.json";
    private string PREFKEY = "score_json_data";


    public  ScoreDataList UpdateScoreData(ScoreData newScoreData)
    {
       
        ScoreDataList pastDatas = ReadData();
        if (pastDatas == null)
        {
            pastDatas = new ScoreDataList();
        }
        
        pastDatas.scoredataList.Add(newScoreData);
        ScoreDataList orderedList = new ScoreDataList
        {
            scoredataList = pastDatas.scoredataList.OrderByDescending(data => data.score).ToList()
        };
        
        
        
        

            

        if (orderedList.scoredataList.Count > 10)
        {
            orderedList.scoredataList.RemoveAt(orderedList.scoredataList.Count - 1); 
        }


        SaveData(orderedList); 

       return  orderedList;

    }

  
    public void SaveData(ScoreDataList scoreData)
    {
        var json = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString(PREFKEY,json);
        PlayerPrefs.Save();
    }


    
    public ScoreDataList ReadData()
    {
        var json = PlayerPrefs.GetString(PREFKEY);
        var obj = JsonUtility.FromJson<ScoreDataList>(json);
        return obj; 
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteKey(PREFKEY);
    }
}