using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Round Completion Settings")]
    [SerializeField] private int _minCompletionMoney = 3;
    [SerializeField] private int _maxCompletionMoney = 5;
    private int _completionMoney;

    private int _currentRound;
    
    public static event Action<int> RoundChanged;

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStart;
        ScoreManager.ThresholdReached += OnThresholdReached;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStart;
        ScoreManager.ThresholdReached -= OnThresholdReached;
    }

    private void OnGameStart()
    {
        _currentRound = 1;
        SetNewCompletionMoney();
    }

    private void OnThresholdReached()
    {
        IncrementRound();
        SetNewCompletionMoney();
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

    void SetNewCompletionMoney()
    {
        System.Random rng = new();
        _completionMoney = rng.Next(_minCompletionMoney, _maxCompletionMoney + 1);
    }

    public int GetCompletionMoney()
    {
        return _completionMoney;
    }
}
