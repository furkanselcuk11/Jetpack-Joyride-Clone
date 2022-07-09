using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;

    public bool gameStart;
    public bool isFinish;
    private int meter;
    private int coin;

    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    void Start()
    {
        gameStart = false;
        isFinish = false;
        Time.timeScale = 1;
        AudioController.audioControllerInstance.Play("BGSound");
        StartTextReset();
    }
    void StartTextReset()
    {
        meter = 0;
        coin = 0;
        UIController.uicontrollerInstance.GamePlayMeterText.text = meter.ToString() + " m";
        UIController.uicontrollerInstance.GamePlayGoldText.text = coin.ToString();
    }
    void Update()
    {
        
    }    
    public void AddCoin()
    {
        // Altýn Ekle
        Debug.Log("Coin added");
        AudioController.audioControllerInstance.Play("CoinSound");
        coin++;
        UIController.uicontrollerInstance.GamePlayGoldText.text = coin.ToString(); ;
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
    public void NextLevel()
    {
        // Sonraki Level
        gameStart = false;
    }
    public void RetyLevel()
    {
        // Yeniden baþlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GamePausePlay(int value)
    {
        Time.timeScale = value;
        // Oyunu durdur
    }
    public void GameExit()
    {
        // Oyundan çýk
        Application.Quit();
    }
    public IEnumerator MeterCounter()
    {
        while (true)
        {

            yield return new WaitForSeconds(0.1f);
            meter += 1;
            UIController.uicontrollerInstance.GamePlayMeterText.text = meter.ToString() + " m";
        }

    }
}
