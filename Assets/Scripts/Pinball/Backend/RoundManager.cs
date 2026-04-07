using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    /*
    [Header("Backend Controllers/Managers")]
    [SerializeField] private UIManager UI;
    */
    
    [Header("Round Completion Settings")]
    [SerializeField] private int _minCompletionMoney = 3;
    [SerializeField] private int _maxCompletionMoney = 5;
    private int _completionMoney = 0;

    private int _currentRound = 1;
    
    // Orchestrator events. Other scripts should react to the beginning and ending of rounds.
    public static event Action RoundStart;
    public static event Action RoundOver;

    /* Event Subscriptions */

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStart;
        GameController.IntermissionEnded += OnIntermissionEnded;

        ScoreManager.ThresholdReached += OnThresholdReached;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStart;
        GameController.IntermissionEnded -= OnIntermissionEnded;

        ScoreManager.ThresholdReached -= OnThresholdReached;
    }

    /* Event Reactions */

    private void OnGameStart()
    {
        Debug.Log("RoundManager | OnGameStart: Round set to 1.");

        _currentRound = 1;
        InitializeRoundStart();
    }

    private void OnIntermissionEnded()
    {
        _currentRound++;

        Debug.Log($"RoundManager | OnIntermissionEnded: Round set to {_currentRound}.");

        InitializeRoundStart();
    }

    private void OnThresholdReached()
    {
        RoundOver?.Invoke();
    }

    /* Script-Specific Methods */

    private void InitializeRoundStart()
    {
        Debug.Log("RoundManager | InitializeRoundStart: Round elements initialized; RoundStart invoked.");

        SetNewCompletionMoney();

        RoundStart?.Invoke();
    }

    /* Getters and Setters */

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
