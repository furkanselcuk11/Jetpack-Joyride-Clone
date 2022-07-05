using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        gameStart = false;
        isFinish = false;
        Time.timeScale = 1;
    }
    void Update()
    {
        
    }    
    public void AddCoin()
    {
        // Alt�n Ekle
    }
    public void Power()
    {
        // Power A��ls�n
    }
    public void NextLevel()
    {
        // Sonraki Level
        gameStart = false;
    }
    public void RetyLevel()
    {
        // Yeniden ba�lat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GamePausePlay(int value)
    {
        Time.timeScale = value;
        // Oyunu durdur
    }
    public void GameExit()
    {
        // Oyundan ��k
        Application.Quit();
    }
}
