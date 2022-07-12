using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jetpack Type", menuName = "JetpackSO")]
public class JetpackSO : ScriptableObject
{
    [SerializeField] private int _selectedJetpack = 0;

    [SerializeField] private JetpackPrint[] _jetpacks;
    public int selectedJetpack
    {
        get { return _selectedJetpack; }
        set { _selectedJetpack = value; }
    }
    public JetpackPrint[] jetpacks
    {
        get { return _jetpacks; }
        set { _jetpacks = value; }
    }
}

[System.Serializable]
public class JetpackPrint
{
    public int index;
    public int price;

    public bool isUnlocked;
}
