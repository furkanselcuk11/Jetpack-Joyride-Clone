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
        StartCoroutine(nameof(StartLoading));
    }
    IEnumerator StartLoading()
    {
        if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index deðerine göre) son seviye gelince ilk levele geri döner
        {
            AsyncOperation async= SceneManager.LoadSceneAsync(1);  // Oyunun ilk sahnesinin index degerini çaliştirir
            scoreType.gameLevel = 1;    // Oyun levelini 1 yapar
            while (!async.isDone)
            {
                progressBar.value = async.progress; // Loading akranı ileriler
                yield return null;
            }
        }
        else
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(scoreType.gameLevel);   
            //Bir sonraki levele geçer
            while (!async.isDone)
            {
                progressBar.value = async.progress; // Loading akranı ileriler
                yield return null;
            }
        }
        
    }
}
