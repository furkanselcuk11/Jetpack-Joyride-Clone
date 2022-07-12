using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 
    [SerializeField] private ProfilSO profilType = null;    // Scriptable Objects eriþir 

    public static GameManager gamemanagerInstance;

    public bool gameStart;  // Oyun basladimi
    public bool isFinish;   // Finish alanına girdimi

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
        gameStart = false;  // oyun yuklendiginde gameStart false olarak baslar
        isFinish = false; // oyun yuklendiginde isFinish false olarak baslar
        Time.timeScale = 1; // oyun zamanı normal hızda başlat
        AudioController.audioControllerInstance.Play("BGSound");    // oyun ana sesini acar
        StartTextReset();   // ana ekrandaki text yazıları gunceller
    }
    void StartTextReset()
    {
        // ana ekrandaki text yazıları gunceller
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
        // Altın Ekler
        Debug.Log("Coin added");
        AudioController.audioControllerInstance.Play("CoinSound");  // Coin ses acar
        scoreType.currentCoin++;    
        UIController.uicontrollerInstance.GamePlayGoldText.text = scoreType.currentCoin.ToString();
        // Coin ekler ve text gunceller
    }

    public void Music(int value)
    {    
        // Müzik aç/kapat
        if (value == 0)
        {
            AudioController.audioControllerInstance.Stop("BGSound");
            // Muzik kapatır
        }
        else
        {
            AudioController.audioControllerInstance.Play("BGSound");
            // Muzik kapatır
        }
        SaveManager.savemanagerInstance.SaveGAme(); // Muzık ayarlarını kaydeder
    }
    public void Finish()
    {
        isFinish = true;    
        StopCoroutine("MeterCounter");  // mesafe sayaci durdurulur      
        UIController.uicontrollerInstance.WinPanelActive();    // LosePanel Açıl 
        UIController.uicontrollerInstance.WinPanelText();        // WinPanel Açıl
    }
    public void NextLevel()
    {
        // Sonraki Level gecer
        gameStart = false;
        scoreType.totalCoin += scoreType.currentCoin;   // oyunda toplanan coinleri totalcoin kismina ekler
        scoreType.totalMeter += scoreType.currentMeter; // oyunda gidilen mesafeyi totalmeter kismina ekler
        scoreType.gameLevel++;  // Bir sonraki icin arttırır
        if (scoreType.gameLevel == SceneManager.sceneCountInBuildSettings)  // Son seviye kaçsa (index deðerine göre ) son seviye gelince ilk levele geri döner
        {
            SceneManager.LoadScene(1);  // Oyunun ilk sahnesinin index degerini çalistirir
            scoreType.gameLevel = 1;    // oyunlevelini 1 oalrak ekler
        }
        else
        {
            SceneManager.LoadScene(scoreType.gameLevel);   // Currentevel+1 diye deðiþtir
            //Bir sonraki levele geçer
        }
        if (scoreType.totalMeter >= profilType.levelUPMeter)
        {
            // Eğer Toplam gidilen mesafe level atlamak için gerekli olan toplam mesafeye eşit veya büyük ise
            // Level atla ve Shiled süresisi uzat 
            // Level atlamak için gerekli olan toplam mesafeyi arttır
            profilType.level++;
            profilType.shield++;
            profilType.levelUPMeter+= profilType.levelUPMeter;
        }
        SaveManager.savemanagerInstance.SaveGAme();
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
            // Mesafe sayacını baslatir
            yield return new WaitForSeconds(0.1f);
            scoreType.currentMeter += 1;
            UIController.uicontrollerInstance.GamePlayMeterText.text = scoreType.currentMeter.ToString() + " m";
        }

    }
}
