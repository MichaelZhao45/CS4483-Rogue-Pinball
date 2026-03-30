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

    // Orchestrator events.
    public static event Action GameStarted;
    public static event Action GameOver;
    //public static event Action GameRestarted;

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
        _ballsRemaining = 3;
        UI.SetBalls(_ballsRemaining);

        gameInProgress = true;
        GameStarted?.Invoke();
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
        GameOver?.Invoke();
    }
}
