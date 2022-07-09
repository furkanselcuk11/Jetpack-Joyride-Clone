using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Profil Type", menuName = "ProfilSO")]
public class ProfilSO : ScriptableObject
{
    [SerializeField] private int _level=1;
    [SerializeField] private float _shield=5f;
    [SerializeField] private int _levelUPMeter=1000;

    public int level
    {
        get { return _level; }
        set { _level = value; }
    }
    public float shield
    {
        get { return _shield; }
        set { _shield = value; }
    }
    public int levelUPMeter
    {
        get { return _levelUPMeter; }
        set { _levelUPMeter = value; }
    }
}
