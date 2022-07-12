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

    [SerializeField] private int currentCharacterIndex;
    [SerializeField] private int currentJetpackIndex;
    public GameObject[] characterModels;
    [SerializeField] private GameObject[] jetpackModels;
    [SerializeField] private Button[] buyCharacterButtons;
    [SerializeField] private Button[] buyJetpackButtons;

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
    }

    
    void Update()
    {
        
    }
    public void ChangeCharacter(int newCharacter)
    {
        characterModels[currentCharacterIndex].SetActive(false);
        characterModels[newCharacter].SetActive(true);
        characterType.selectedCharacter = newCharacter;
        player.GetComponent<PlayerController>().anim= characterModels[newCharacter].gameObject.GetComponent<Animator>();
        SaveManager.savemanagerInstance.SaveGAme();
    }
    public void ChangeJetpack(int newJetpack)
    {
        jetpackModels[currentJetpackIndex].SetActive(false);
        jetpackModels[newJetpack].SetActive(true);
        jetpackType.selectedJetpack = newJetpack;
        SaveManager.savemanagerInstance.SaveGAme();
    }
    public void CharacterUpdate()
    {
        currentCharacterIndex = characterType.selectedCharacter;
        foreach (GameObject character in characterModels)
        {
            character.SetActive(false);
        }
        characterModels[currentCharacterIndex].SetActive(true);
        SaveManager.savemanagerInstance.SaveGAme();
    }
    public void JetpackUpdate()
    {
        currentJetpackIndex = jetpackType.selectedJetpack;
        foreach (GameObject jetpack in jetpackModels)
        {
            jetpack.SetActive(false);
        }
        jetpackModels[currentJetpackIndex].SetActive(true);
        SaveManager.savemanagerInstance.SaveGAme();
    }
    public void UpdateButtons()
    {
        for (int i = 0; i < characterType.characters.Length; i++)
        {
            if (characterType.characters[i].isUnlocked)
            {
                buyCharacterButtons[i].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
                buyCharacterButtons[i].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            }
            else
            {
                buyCharacterButtons[i].gameObject.SetActive(true);   // Eğer Ball alınmamış (isUnlocked) ise satın alma tuşu aktif olacak
                buyCharacterButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "BUY " + characterType.characters[i].price;    // Satın alınacak ball fiyatı
                buyCharacterButtons[i].transform.parent.GetComponent<Button>().interactable = false;       // Eğer Ball alınmamış (isUnlocked)ise  ball seçilemez olacak    

            }
        }
        for (int i = 0; i < jetpackType.jetpacks.Length; i++)
        {
            if (jetpackType.jetpacks[i].isUnlocked)
            {
                buyJetpackButtons[i].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
                buyJetpackButtons[i].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            }
            else
            {
                buyJetpackButtons[i].gameObject.SetActive(true);   // Eğer Ball alınmamış (isUnlocked) ise satın alma tuşu aktif olacak
                buyJetpackButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "BUY " + jetpackType.jetpacks[i].price;    // Satın alınacak ball fiyatı
                buyJetpackButtons[i].transform.parent.GetComponent<Button>().interactable = false;       // Eğer Ball alınmamış (isUnlocked)ise  ball seçilemez olacak    

            }
        }
        SaveManager.savemanagerInstance.SaveGAme();
    }
    public void CharacterBuy(int newcharater)
    {
        if (scoreType.totalCoin >= characterType.characters[newcharater].price)
        {
            characterType.characters[newcharater].isUnlocked = true;
            buyCharacterButtons[newcharater].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
            buyCharacterButtons[newcharater].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            scoreType.totalCoin -= characterType.characters[newcharater].price;
            UIController.uicontrollerInstance.TotalGoldText.text = scoreType.totalCoin.ToString();
        }
        else
        {
            characterType.characters[newcharater].isUnlocked = false;
        }
        SaveManager.savemanagerInstance.SaveGAme();
    }
    public void JetpackBuy(int newJetpack)
    {
        if (scoreType.totalCoin >= jetpackType.jetpacks[newJetpack].price)
        {
            jetpackType.jetpacks[newJetpack].isUnlocked = true;
            buyJetpackButtons[newJetpack].gameObject.SetActive(false);  // Eğer Ball alınmış (isUnlocked) ise satın alma tuşu pasif olacak
            buyJetpackButtons[newJetpack].transform.parent.GetComponent<Button>().interactable = true;  // Eğer Ball alınmış (isUnlocked) ise  ball seçilebilir olacak
            scoreType.totalCoin -= jetpackType.jetpacks[newJetpack].price;
            UIController.uicontrollerInstance.TotalGoldText.text = scoreType.totalCoin.ToString();
        }
        else
        {
            jetpackType.jetpacks[newJetpack].isUnlocked = false;
        }
        SaveManager.savemanagerInstance.SaveGAme();
    }
}
