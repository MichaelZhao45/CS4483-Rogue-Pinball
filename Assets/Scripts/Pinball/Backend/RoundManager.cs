using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private int _currentRound;

    public static event Action<int> RoundChanged;

    private void OnEnable()
    {
        GameController.GameStarted += Initialize;
        ScoreManager.ThresholdReached += IncrementRound;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= Initialize;
        ScoreManager.ThresholdReached -= IncrementRound;
    }

    void Initialize()
    {
        _currentRound = 1;
    }

    private void IncrementRound()
    {
        _currentRound++;
        RoundChanged?.Invoke(_currentRound);
    }

    public int GetCurrentRound()
    {
        return _currentRound;
    }
}
