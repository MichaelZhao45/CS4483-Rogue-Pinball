using System;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager UI;

    [Header("Settings")]
    [SerializeField] private int _scoreIncreaseFactor = 10;
    [SerializeField] private int _startingScoreThreshold = 100;
    
    private int _currentScore;
    private int _scoreThreshold;

    private int _scoreMultiplier = 1;

    // Signals that the player has met the current score threshold.
    public static event Action ThresholdReached;

    /* Event Subscriptions */

    private void OnEnable()
    {
        Bumper.OnBumperHit += OnBumperHit;

        GameController.GameStarted += Reset;
        RoundManager.RoundStart += OnRoundStart;
        RoundManager.RoundOver += OnRoundOver;
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit -= OnBumperHit;

        GameController.GameStarted -= Reset;
        RoundManager.RoundStart -= OnRoundStart;
        RoundManager.RoundOver -= OnRoundOver;
    }

    /* Event Reactions */

    private void OnBumperHit(int scoreGained)
    {
        AddScore(scoreGained * _scoreMultiplier);
        CheckRoundComplete();
    }

    private void OnRoundStart(int round)
    {
        SetScore(0);
        // The threshold follows the formula: y = factor * (x - 1)^2 + threshold.
        SetThreshold(_scoreIncreaseFactor * (int)Math.Pow(round - 1, 2) + _startingScoreThreshold);
    }

    private void OnRoundOver()
    {
        ResetMultiplier();
    }

    /* Script-Specific Methods */

    public void AddScore(int gainedScore)
    {
        _currentScore += gainedScore;
        UI.SetScore(_currentScore);
    }

    public void ResetMultiplier()
    {
        _scoreMultiplier = 1;
    }

    public void AddMultiplier(int increase)
    {
        _scoreMultiplier += increase;
    }

    private void CheckRoundComplete()
    {
        if (_currentScore >= _scoreThreshold)
        {
            ThresholdReached?.Invoke();
        }
    }

    private void Reset()
    {
        ResetMultiplier();
        SetScore(0);
        SetThreshold(_startingScoreThreshold);
    }

    /* Getters and Setters */

    public int GetScore()
    {
        return _currentScore;
    }

    public void SetScore(int newScore)
    {
        _currentScore = newScore;
        UI.SetScore(_currentScore);
    }

    public int GetScoreThreshold()
    {
        return _scoreThreshold;
    }

    public void SetThreshold(int threshold)
    {
        _scoreThreshold = threshold;
        UI.SetThreshold(_scoreThreshold);
    }
}
