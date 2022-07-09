using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rocket Type", menuName = "RocketSO")]
public class RocketSO : ScriptableObject
{
    [SerializeField] private int _selectedRocket = 0;

    [SerializeField] private RocketPrint[] _rockets;
    public int selectedRocket
    {
        get { return _selectedRocket; }
        set { _selectedRocket = value; }
    }
    public RocketPrint[] rockets
    {
        get { return _rockets; }
        set { _rockets = value; }
    }
}

[System.Serializable]
public class RocketPrint
{
    public int index;
    public int price;

    public bool isUnlocked;
}
