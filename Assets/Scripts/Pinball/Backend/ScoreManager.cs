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
        SetScore(0);
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
}
