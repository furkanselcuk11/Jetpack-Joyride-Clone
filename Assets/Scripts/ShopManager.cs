using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private CharacterSO characterType = null;    // Scriptable Objects eriþir 
    [SerializeField] private JetpackSO jetpackType = null;    // Scriptable Objects eriþir 
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 

    public static ShopManager shopmanagerInstance;
    [SerializeField] private GameObject player;

    [SerializeField] private int currentCharacterIndex; // aktif olan karakter
    [SerializeField] private int currentJetpackIndex;   // aktif olan jetpack
    public GameObject[] characterModels;    // karakter modelleri
    [SerializeField] private GameObject[] jetpackModels;    // jetpack modelleri
    [SerializeField] private Button[] buyCharacterButtons;  // karakter satin alma butonlari
    [SerializeField] private Button[] buyJetpackButtons;    // jetpack satin alma butonlari

    private void Awake()
    {
        if (shopmanagerInstance == null)
        {
            shopmanagerInstance = this;
        }
    }
    void Start()
    {
        CharacterUpdate();  
        JetpackUpdate();
        // oyun basladiginda karakter ve jetpack modellerinin hangilerini aktif oldugu guncellenir
    }

    
    void Update()
    {
        
    }
    public void ChangeCharacter(int newCharacter)
    {
        characterModels[currentCharacterIndex].SetActive(false);    // suanda secili olan model pasif olur
        characterModels[newCharacter].SetActive(true);  // yeni secilen model aktif olur
        characterType.selectedCharacter = newCharacter; // kayitlardaki secili karakter yeni secilen karakter olur
        player.GetComponent<PlayerController>().anim= characterModels[newCharacter].gameObject.GetComponent<Animator>();
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
    public void ChangeJetpack(int newJetpack)
    {
        jetpackModels[currentJetpackIndex].SetActive(false);    // suanda secili olan model pasif olur
        jetpackModels[newJetpack].SetActive(true);  // yeni secilen model aktif olur
        jetpackType.selectedJetpack = newJetpack;   // kayitlardaki secili jetpack yeni secilen jetpack olur
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
    public void CharacterUpdate()
    {
        currentCharacterIndex = characterType.selectedCharacter;    // suanki karaktere kayitlardaki secili karakeri atar
        foreach (GameObject character in characterModels)
        {
            character.SetActive(false); // Tum karakterleri pasif yapar
        }
        characterModels[currentCharacterIndex].SetActive(true); // kayitlardaki karakter aktif olur
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
    public void JetpackUpdate()
    {
        currentJetpackIndex = jetpackType.selectedJetpack;  // suanki jetpacke kayitlardaki secili jetpack atar
        foreach (GameObject jetpack in jetpackModels)
        {
            jetpack.SetActive(false);   // Tum jetpackleri pasif yapar
        }
        jetpackModels[currentJetpackIndex].SetActive(true); // kayitlardaki jetpack aktif olur
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
    public void UpdateButtons()
    {
        for (int i = 0; i < characterType.characters.Length; i++)
        {
            if (characterType.characters[i].isUnlocked)
            {
                buyCharacterButtons[i].gameObject.SetActive(false);  // Eğer karakter alınmış (isUnlocked) ise satın alma tuşu pasif olacak
                buyCharacterButtons[i].transform.parent.GetComponent<Button>().interactable = true;  // Eğer karakter alınmış (isUnlocked) ise  karakter seçilebilir olacak
            }
            else
            {
                buyCharacterButtons[i].gameObject.SetActive(true);   // Eğer karakter alınmamış (isUnlocked) ise satın alma tuşu aktif olacak
                buyCharacterButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "BUY " + characterType.characters[i].price;    // Satın alınacak karakter fiyatı
                buyCharacterButtons[i].transform.parent.GetComponent<Button>().interactable = false;       // Eğer karakter alınmamış (isUnlocked)ise  karakter seçilemez olacak    

            }
        }
        for (int i = 0; i < jetpackType.jetpacks.Length; i++)
        {
            if (jetpackType.jetpacks[i].isUnlocked)
            {
                buyJetpackButtons[i].gameObject.SetActive(false);  // Eğer jetpack alınmış (isUnlocked) ise satın alma tuşu pasif olacak
                buyJetpackButtons[i].transform.parent.GetComponent<Button>().interactable = true;  // Eğer jetpack alınmış (isUnlocked) ise  jetpack seçilebilir olacak
            }
            else
            {
                buyJetpackButtons[i].gameObject.SetActive(true);   // Eğer Ball alınmamış (isUnlocked) ise satın alma tuşu aktif olacak
                buyJetpackButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "BUY " + jetpackType.jetpacks[i].price;    // Satın alınacak ball fiyatı
                buyJetpackButtons[i].transform.parent.GetComponent<Button>().interactable = false;       // Eğer jetpack alınmamış (isUnlocked)ise  jetpack seçilemez olacak    

            }
        }
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
    public void CharacterBuy(int newcharater)
    {
        if (scoreType.totalCoin >= characterType.characters[newcharater].price)
        {
            characterType.characters[newcharater].isUnlocked = true;
            buyCharacterButtons[newcharater].gameObject.SetActive(false);  // Eğer karakter alınmış (isUnlocked) ise satın alma tuşu pasif olacak
            buyCharacterButtons[newcharater].transform.parent.GetComponent<Button>().interactable = true;  // Eğer karakter alınmış (isUnlocked) ise  karakter seçilebilir olacak
            scoreType.totalCoin -= characterType.characters[newcharater].price;
            UIController.uicontrollerInstance.TotalGoldText.text = scoreType.totalCoin.ToString();
        }
        else
        {
            characterType.characters[newcharater].isUnlocked = false;
        }
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
    public void JetpackBuy(int newJetpack)
    {
        if (scoreType.totalCoin >= jetpackType.jetpacks[newJetpack].price)
        {
            jetpackType.jetpacks[newJetpack].isUnlocked = true;
            buyJetpackButtons[newJetpack].gameObject.SetActive(false);  // Eğer jetpack alınmış (isUnlocked) ise satın alma tuşu pasif olacak
            buyJetpackButtons[newJetpack].transform.parent.GetComponent<Button>().interactable = true;  // Eğer jetpack alınmış (isUnlocked) ise  jetpack seçilebilir olacak
            scoreType.totalCoin -= jetpackType.jetpacks[newJetpack].price;
            UIController.uicontrollerInstance.TotalGoldText.text = scoreType.totalCoin.ToString();
        }
        else
        {
            jetpackType.jetpacks[newJetpack].isUnlocked = false;
        }
        SaveManager.savemanagerInstance.SaveGAme(); // Son degisiklikleri kayit eder
    }
}
