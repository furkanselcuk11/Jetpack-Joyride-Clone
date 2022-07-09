using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;

    public bool gameStart;
    public bool isFinish;
    public bool isShield;

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
        isShield = false;
        Time.timeScale = 1;
        AudioController.audioControllerInstance.Play("BGSound");
    }
    void Update()
    {
        
    }    
    public void AddCoin()
    {
        // Altýn Ekle
        Debug.Log("Coin added");
        AudioController.audioControllerInstance.Play("CoinSound");
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
}
