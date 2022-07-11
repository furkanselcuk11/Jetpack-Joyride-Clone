using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 
    [SerializeField] private ProfilSO profilType = null;    // Scriptable Objects eriþir 

    public static GameManager gamemanagerInstance;

    public bool gameStart;
    public bool isFinish;

    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    void Start()
    {
        //SceneManager.LoadScene(scoreType.gameLevel);
        gameStart = false;
        isFinish = false;
        Time.timeScale = 1;
        AudioController.audioControllerInstance.Play("BGSound");
        StartTextReset();
    }
    void StartTextReset()
    {
        scoreType.currentMeter = scoreType.minMeter;
        scoreType.currentCoin = scoreType.minCoin;
        UIController.uicontrollerInstance.GamePlayMeterText.text = scoreType.minMeter.ToString() + " m";
        UIController.uicontrollerInstance.GamePlayGoldText.text = scoreType.minCoin.ToString();
    }
    void Update()
    {
        
    }    
    public void AddCoin()
    {
        // Altın Ekle
        Debug.Log("Coin added");
        AudioController.audioControllerInstance.Play("CoinSound");
        scoreType.currentCoin++;
        UIController.uicontrollerInstance.GamePlayGoldText.text = scoreType.currentCoin.ToString(); ;
    }
    
    public void Music(int value)
    {    
        // Müzik aç/kapat
        if (value == 0)
        {
            AudioController.audioControllerInstance.Stop("BGSound");

        }
        else
        {
            AudioController.audioControllerInstance.Play("BGSound");
        }        
    }
    public void Finish()
    {
        isFinish = true;
        StopCoroutine("MeterCounter");
        FindObjectOfType<Spawner>().SpawnStop();
        UIController.uicontrollerInstance.WinPanelActive();    // LosePanel Açıl // WinPanel Açıl
        UIController.uicontrollerInstance.WinPanelText();
    }
    public void NextLevel()
    {
        // Sonraki Level
        gameStart = false;
        scoreType.totalCoin += scoreType.currentCoin;
        scoreType.totalMeter += scoreType.currentMeter;
        scoreType.gameLevel++;
        if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index deðerine göre 2) son seviye gelince ilk levele geri döner
        {
            SceneManager.LoadScene(0);  // Oyunun ilk sahnesinin Ýndex deðerini çalýþtýrýr
            scoreType.gameLevel = 0;
        }
        else
        {
            SceneManager.LoadScene(scoreType.gameLevel);   // Currentevel+1 diye deðiþtir
            //Bir sonraki levele geçer
        }
    }
    public void RetyLevel()
    {
        // Yeniden başlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GamePausePlay(int value)
    {
        Time.timeScale = value;
        // Oyunu durdur
    }
    public void GameExit()
    {
        // Oyundan çık
        Application.Quit();
    }
    public IEnumerator MeterCounter()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            scoreType.currentMeter += 1;
            UIController.uicontrollerInstance.GamePlayMeterText.text = scoreType.currentMeter.ToString() + " m";
        }

    }
}
