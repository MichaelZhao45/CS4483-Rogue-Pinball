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
    private bool _gameInProgress = false;

    private int _maxBalls = 2;

    // Orchestrator events.
    public static event Action GameStarted;
    public static event Action GameEnded;
    public static event Action IntermissionStarted;
    public static event Action IntermissionEnded;

    private void OnEnable()
    {
        Drain.OnDrainHit += OnBallDrained;
        RoundManager.RoundStart += OnRoundStart;
        RoundManager.RoundOver += OnRoundOver;
    }

    private void OnDisable()
    {
        Drain.OnDrainHit -= OnBallDrained;
        RoundManager.RoundStart -= OnRoundStart;
        RoundManager.RoundOver -= OnRoundOver;
    }

    public IEnumerator DelayStartGame(float time)
    {
        _gameInProgress = true;
        yield return new WaitForSeconds(time);
        StartGame();
    }

    public void StartGame()
    {
        _gameInProgress = true;
        GameStarted?.Invoke();
    }

    // Used to begin the intermission period.
    public void StartIntermission()
    {
        IntermissionStarted?.Invoke();
    }

    // Used to end the intermission period.
    public void EndIntermission()
    {
        IntermissionEnded?.Invoke();
    }

    private void OnBallDrained()
    {
        _ballsRemaining--;
        UI.SetBalls(_ballsRemaining);
        
        if (_ballsRemaining >= 0) dropper.SetDropperActive(true);
        else HandleGameOver();
    }

    private void OnRoundStart()
    {
        // Reset the ball counter back to full for the next round.
        _ballsRemaining = _maxBalls;
        UI.SetBalls(_ballsRemaining);
    }

    private void OnRoundOver()
    {
        // Pause the game for round results and shop intermission.
        StartIntermission();
    }

    private void HandleGameOver()
    {
        _gameInProgress = false;
        GameEnded?.Invoke();
    }

    public bool IsGameInProgress()
    {
        return _gameInProgress;
    }
}
