using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreType = null;    // Scriptable Objects eriþir 
    [SerializeField] private ProfilSO profilType = null;    // Scriptable Objects eriþir
    
    [SerializeField] private Image fillAmountImage;
    
    void Update()
    {
        GetCurrentFill();   // Progresbar ilerler
    }
    void GetCurrentFill()
    {
        // FillAmount degeri toplam mesafe/ toplam gidilmesi gereken mesafe oalrak girer
        float fillAmount = (float)scoreType.totalMeter / (float)profilType.levelUPMeter;   
        fillAmountImage.fillAmount = fillAmount;
    }
}
