using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int _lastRound = 20;

    [Header("Round Completion Settings")]
    private int _completionMoney = 0;

    private int _currentRound = 1;
    
    /* Orchestrator events. Other scripts should react to the beginning and ending of rounds. */

    // Signals that the round is set up and ready to begin.
    public static event Action<int> RoundStart;
    // Signals that the round has been completed, and that the "intermission period" should begin.
    public static event Action RoundOver;
    // Signals that the last (maximum) round has been completed.
    // Other scripts will interpret this as the game being won.
    public static event Action LastRoundOver;

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
        if (_currentRound < _lastRound)
        {
            Debug.Log($"RoundManager | OnScoreThresholdReached: Round {_currentRound} completed.");
            RoundOver?.Invoke();
        }
        else
        {
            Debug.Log($"RoundManager | OnScoreThresholdReached: Final round {_currentRound} completed.");
            LastRoundOver?.Invoke();
        }
    }

    /* Script-Specific Methods */

    private void InitializeRoundStart()
    {
        Debug.Log("RoundManager | InitializeRoundStart: Round elements initialized; RoundStart invoked.");

        RoundStart?.Invoke(_currentRound);
    }

    /* Getters and Setters */

    public int GetCurrentRound()
    {
        return _currentRound;
    }

    public int GetCompletionMoney()
    {
        return _completionMoney;
    }
}
