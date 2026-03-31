using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIManager UI;
    public BallDropper dropper;
    public ScoreManager scoreManager;
    public RoundManager roundManager;
    
    private int _ballsRemaining;
    public bool gameInProgress = false;

    private int _maxBalls = 2;

    // Orchestrator events.
    public static event Action GameStarted;
    public static event Action GameOver;
    //public static event Action GameRestarted;

    private void OnEnable()
    {
        Drain.OnDrainHit += OnBallDrained;
        RoundManager.RoundChanged += OnRoundChanged;
    }

    private void OnDisable()
    {
        Drain.OnDrainHit -= OnBallDrained;
        RoundManager.RoundChanged -= OnRoundChanged;
    }

    public IEnumerator DelayStartGame(float time)
    {
        gameInProgress = true;
        yield return new WaitForSeconds(time);
        StartGame();
    }

    public void StartGame()
    {
        _ballsRemaining = _maxBalls;
        UI.SetBalls(_ballsRemaining);

        gameInProgress = true;
        GameStarted?.Invoke();
    }

    private void OnBallDrained()
    {
        _ballsRemaining--;
        UI.SetBalls(_ballsRemaining);
        
        if (_ballsRemaining >= 0) dropper.SetDropperActive(true);
        else HandleGameOver();
    }

    private void OnRoundChanged(int round)
    {
        _ballsRemaining = _maxBalls;
        UI.SetBalls(_ballsRemaining);
    }

    private void HandleGameOver()
    {
        gameInProgress = false;
        GameOver?.Invoke();
    }
}
