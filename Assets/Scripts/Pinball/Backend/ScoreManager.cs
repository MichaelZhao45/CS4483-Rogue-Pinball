using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager UI;
    
    private int _currentScore;
    private int _scoreThreshold;

    public static event Action ThresholdReached;

    private void OnEnable()
    {
        Bumper.OnBumperHit += AddScore;
        GameController.GameStarted += Initialize;
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit -= AddScore;
        GameController.GameStarted -= Initialize;
    }

    void Initialize()
    {
        SetScore(0);
        SetThreshold(250);
    }

    void Update()
    {
        if (_currentScore >= _scoreThreshold)
        {
            ThresholdReached?.Invoke();
            SetScore(0);
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
    }
}
