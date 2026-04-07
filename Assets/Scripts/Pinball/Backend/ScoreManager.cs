using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager UI;

    [SerializeField] private int _startingScoreThreshold = 100;
    
    private int _currentScore;
    private int _scoreThreshold;

    public static event Action ThresholdReached;

    /* Event Subscriptions */

    private void OnEnable()
    {
        Bumper.OnBumperHit += OnBumperHit;

        GameController.GameStarted += Initialize;
        GameController.IntermissionEnded += Initialize;
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit -= OnBumperHit;

        GameController.GameStarted -= Initialize;
        GameController.IntermissionEnded -= Initialize;
    }

    /* Event Reactions */

    private void OnBumperHit(int scoreGained)
    {
        AddScore(scoreGained);
        CheckRoundComplete();
    }

    /* Script-Specific Methods */

    public void AddScore(int gainedScore)
    {
        _currentScore += gainedScore;
        UI.SetScore(_currentScore);
    }

    private void CheckRoundComplete()
    {
        if (_currentScore >= _scoreThreshold)
        {
            ThresholdReached?.Invoke();
            // TODO: non-linear increase?
            SetThreshold(_scoreThreshold + 250);
        }
    }

    private void Initialize()
    {
        SetScore(0);
        SetThreshold(_startingScoreThreshold);
    }

    /* Getters and Setters */

    public int GetScore()
    {
        return _currentScore;
    }

    public int GetScoreThreshold()
    {
        return _scoreThreshold;
    }

    public void SetScore(int newScore)
    {
        _currentScore = newScore;
        UI.SetScore(_currentScore);
    }

    public void SetThreshold(int threshold)
    {
        _scoreThreshold = threshold;
        UI.SetThreshold(_scoreThreshold);
    }
}
