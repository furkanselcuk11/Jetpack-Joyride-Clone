using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 
    [SerializeField] private Slider progressBar;
    void Start()
    {
        //if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index deðerine göre 2) son seviye gelince ilk levele geri döner
        //{
        //    SceneManager.LoadScene(1);  // Oyunun ilk sahnesinin Ýndex deðerini çalýþtýrýr
        //    scoreType.gameLevel = 1;
        //}
        //else
        //{
        //    SceneManager.LoadScene(scoreType.gameLevel);   // Currentevel+1 diye deðiþtir
        //    //Bir sonraki levele geçer
        //}
        StartCoroutine(nameof(StartLoading));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartLoading()
    {
        if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index deðerine göre 2) son seviye gelince ilk levele geri döner
        {
            AsyncOperation async= SceneManager.LoadSceneAsync(1);  // Oyunun ilk sahnesinin Ýndex deðerini çalýþtýrýr
            scoreType.gameLevel = 1;
            while (!async.isDone)
            {
                progressBar.value = async.progress;
                yield return null;
            }
        }
        else
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(scoreType.gameLevel);   // Currentevel+1 diye deðiþtir
            //Bir sonraki levele geçer
            while (!async.isDone)
            {
                progressBar.value = async.progress;
                yield return null;
            }
        }
        
    }
}
