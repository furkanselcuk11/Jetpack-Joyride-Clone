using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scoret Type", menuName = "ScoreSO")]
public class ScoreSO : ScriptableObject
{
    [SerializeField] private int _minCoin;
    [SerializeField] private int _currentCoin;
    [SerializeField] private int _totalCoin;
    [SerializeField] private int _minMeter;
    [SerializeField] private int _currentMeter;
    [SerializeField] private int _totalMeter;

    public int minCoin
    {
        get { return _minCoin; }
        set { _minCoin = value; }
    }
    public int currentCoin
    {
        get { return _currentCoin; }
        set { _currentCoin = value; }
    }
    public int totalCoin
    {
        get { return _totalCoin; }
        set { _totalCoin = value; }
    }
    public int minMeter
    {
        get { return _minMeter; }
        set { _minMeter = value; }
    }
    public int currentMeter
    {
        get { return _currentMeter; }
        set { _currentMeter = value; }
    }
    public int totalMeter
    {
        get { return _totalMeter; }
        set { _totalMeter = value; }
    }
}
