using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager UI;

    [SerializeField] private int _startingScoreThreshold = 100;
    
    private int _currentScore;
    private int _scoreThreshold;

    public static event Action ThresholdReached;

    private void OnEnable()
    {
        Bumper.OnBumperHit += OnBumperHit;
        GameController.GameStarted += Initialize;
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit -= OnBumperHit;
        GameController.GameStarted -= Initialize;
    }

    private void Initialize()
    {
        SetScore(0);
        SetThreshold(_startingScoreThreshold);
    }

    private void OnBumperHit(int scoreGained)
    {
        AddScore(scoreGained);
        CheckRoundComplete();
    }

    private void CheckRoundComplete()
    {
        if (_currentScore >= _scoreThreshold)
        {
            ThresholdReached?.Invoke();
            //SetScore(0);
            // TODO: non-linear increase?
            SetThreshold(_scoreThreshold + 250);
        }
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public void AddScore(int gainedScore)
    {
        _currentScore += gainedScore;
        UI.SetScore(_currentScore);
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
