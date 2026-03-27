using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager UI;
    
    private int _currentScore;
    private int _scoreThreshold;

    private void OnEnable()
    {
        Bumper.OnBumperHit += AddScore;
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit -= AddScore;
    }

    void Start()
    {
        _currentScore = 0;
        SetScore(_currentScore);
    }

    /*
    void Update()
    {
        if (_currentScore >= _scoreThreshold)
        {
            IncrementRound();
        }
    }
    */

    private void AddScore(int gainedScore)
    {
        _currentScore += gainedScore;
        UI.SetScore(_currentScore);
    }

    private void SetScore(int newScore)
    {
        _currentScore = newScore;
        UI.SetScore(_currentScore);
    }
}
