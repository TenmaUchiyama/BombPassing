using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombSceneManager : MonoBehaviour
{
  
    


    public void LoadMainMenu()
    {
        StartCoroutine(ChangeSceneWithArguments(0,0.2f));
    }

    public void LoadPlay()
    {
        StartCoroutine(ChangeSceneWithArguments(1,0.2f));
    }
    public void LoadGameOver()
    {
        StartCoroutine(ChangeSceneWithArguments(2,0.2f));
    }

    public void Instruction()
    {

    }

    private IEnumerator ChangeSceneWithArguments(int sceneNum,  float delay)
    {
        // クリック音の長さ分だけ待機
        yield return new WaitForSecondsRealtime(delay);

        // 引数を使用してシーンを切り替える
        SceneManager.LoadScene(sceneNum);
    }
}
