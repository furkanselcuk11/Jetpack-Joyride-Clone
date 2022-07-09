using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Type", menuName = "CharacterSO")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] private int _selectedCharacter = 0;

    [SerializeField] private CharacterPrint[] _characters;
    public int selectedCharacter
    {
        get { return _selectedCharacter; }
        set { _selectedCharacter = value; }
    }
    public CharacterPrint[] characters
    {
        get { return _characters; }
        set { _characters = value; }
    }
}

[System.Serializable]
public class CharacterPrint
{
    public int index;
    public int price;

    public bool isUnlocked;
}
