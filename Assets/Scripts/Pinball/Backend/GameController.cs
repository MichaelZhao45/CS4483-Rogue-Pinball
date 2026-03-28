using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIManager UI;
    public BallDropper dropper;
    public ScoreManager score;

    private int _currentRound;
    private int _ballsRemaining;
    public bool gameInProgress = false;

    private void OnEnable()
    {
        Drain.OnDrainHit += OnBallDrained;
    }

    private void OnDisable()
    {
        Drain.OnDrainHit -= OnBallDrained;
    }

    public IEnumerator DelayStartGame(float time)
    {
        gameInProgress = true;
        yield return new WaitForSeconds(time);
        StartGame();
    }

    public void StartGame()
    {
        _currentRound = 1;
        _ballsRemaining = 3;

        UI.ShowGameInterface(true);
        UI.SetBalls(_ballsRemaining);

        score.SetScore(0);

        dropper.SetDropperActive(true);

        gameInProgress = true;
    }

    private void OnBallDrained()
    {
        _ballsRemaining--;
        UI.SetBalls(_ballsRemaining);
        
        if (_ballsRemaining > 0) dropper.SetDropperActive(true);
        else HandleGameOver();
    }

    private void HandleGameOver()
    {
        gameInProgress = false;

        UI.ShowGameInterface(false);
        UI.ShowGameOver(true);
    }
}
