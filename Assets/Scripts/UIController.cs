using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 
    [SerializeField] private ProfilSO profilType = null;    // Scriptable Objects eriþir 

    public static UIController uicontrollerInstance;

    [Space]
    [Header("Panel Controller")]
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject GamePlayPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject PausePanel;
    
    [Space]
    [Header("StartPanelText Controller")]
    public TextMeshProUGUI TotalGoldText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ProfilLevelText;
    public TextMeshProUGUI ShieldText;
    public TextMeshProUGUI TotalMeterText;
    [Header("GamePlayPanelText Controller")]
    public TextMeshProUGUI GamePlayGoldText;
    public TextMeshProUGUI GamePlayMeterText;
    [Header("WinPanelText Controller")]    
    public TextMeshProUGUI WinMeterText;
    public TextMeshProUGUI WinGoldText;   
    

    private void Awake()
    {
        if (uicontrollerInstance == null)
        {
            uicontrollerInstance = this;
        }
    }
    void Start()
    {
        StartUI();
        StartPanelText();
    }
    
    void Update()
    {
        
    }

    public void StartUI()
    {
        StartPanel.SetActive(true);
        GamePlayPanel.SetActive(false);
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
        PausePanel.SetActive(false);
    }
    public void GamePlayActive()
    {
        StartPanel.SetActive(false);
        GamePlayPanel.SetActive(true);
        LosePanel.SetActive(false);
        WinPanel.SetActive(false);
        PausePanel.SetActive(false);
    }
    public void LosePanelActive()
    {
        StartPanel.SetActive(false);
        GamePlayPanel.SetActive(false);
        LosePanel.SetActive(true);
        WinPanel.SetActive(false);
        PausePanel.SetActive(false);
    }
    public void WinPanelActive()
    {
        StartPanel.SetActive(false);
        GamePlayPanel.SetActive(false);
        LosePanel.SetActive(false);
        WinPanel.SetActive(true);
        PausePanel.SetActive(false);
    }
    public void StartPanelText()
    {
        TotalGoldText.text = scoreType.totalCoin.ToString();
        LevelText.text = profilType.level.ToString();
        ProfilLevelText.text = profilType.level.ToString();
        ShieldText.text = profilType.shield.ToString()+" sec";
        TotalMeterText.text = scoreType.totalMeter.ToString() + " / " + profilType.levelUPMeter.ToString()+" m" ;
    }
    public void GamePlayPanelText()
    {
        GamePlayMeterText.text = scoreType.currentMeter.ToString() + " m";
        GamePlayGoldText.text = scoreType.currentCoin.ToString();
    }
    public void WinPanelText()
    {
        WinMeterText.text = scoreType.currentMeter.ToString() + " m";
        WinGoldText.text = scoreType.currentCoin.ToString();
    }
}
