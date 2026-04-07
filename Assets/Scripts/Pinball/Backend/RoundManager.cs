using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Round Completion Settings")]
    [SerializeField] private int _minCompletionMoney = 3;
    [SerializeField] private int _maxCompletionMoney = 5;
    private int _completionMoney = 0;

    private int _currentRound = 1;
    
    /* Orchestrator events. Other scripts should react to the beginning and ending of rounds. */

    // Signals that the round is set up and ready to begin.
    public static event Action RoundStart;
    // Signals that the round has been completed, and that the "intermission period" should begin.
    public static event Action RoundOver;

    /* Event Subscriptions */

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStart;
        GameController.GameContinued += OnGameContinued;

        ScoreManager.ThresholdReached += OnScoreThresholdReached;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStart;
        GameController.GameContinued -= OnGameContinued;

        ScoreManager.ThresholdReached -= OnScoreThresholdReached;
    }

    /* Event Reactions */

    private void OnGameStart()
    {
        Debug.Log("RoundManager | OnGameStart: Round set to 1.");

        _currentRound = 1;
        InitializeRoundStart();
    }

    private void OnGameContinued()
    {
        _currentRound++;

        Debug.Log($"RoundManager | OnIntermissionEnded: Round set to {_currentRound}.");

        InitializeRoundStart();
    }

    private void OnScoreThresholdReached()
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
