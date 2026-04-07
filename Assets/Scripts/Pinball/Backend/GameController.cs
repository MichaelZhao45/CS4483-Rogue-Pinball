using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Backend Controllers/Managers")]
    public UIManager UI;
    public BallDropper dropper;
    public ScoreManager scoreManager;
    public RoundManager roundManager;
    
    private int _ballsRemaining;
    private bool _gameInProgress = false;

    private int _maxBalls = 2;

    // Orchestrator events. Controls the flow of gameplay states.
    public static event Action GameStarted;
    public static event Action GameEnded;
    public static event Action IntermissionStarted;
    public static event Action IntermissionEnded;

    /* Event Subscriptions */

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

    /* Event Reactions */

    private void OnRoundStart()
    {
        // Reset the ball counter back to full for the next round.
        _ballsRemaining = _maxBalls;
        UI.SetBalls(_ballsRemaining);
    }

    private void OnRoundOver()
    {
        // Pause the game for round results and shop intermission.
        IntermissionStarted?.Invoke();
    }

    private void OnBallDrained()
    {
        _ballsRemaining--;
        UI.SetBalls(_ballsRemaining);
        
        if (_ballsRemaining >= 0) dropper.SetDropperActive(true);
        else HandleGameOver();
    }

    /* Script-Specific Methods */

    public IEnumerator DelayStart(float time)
    {
        yield return new WaitForSeconds(time);
        
        // If a game is not yet in progress, start a new game.
        if (!_gameInProgress)
        {
            Debug.Log("GameController | DelayStart: New game started.");
            _gameInProgress = true;
            GameStarted?.Invoke();
        }
        // If a game is already in progress, resume the game.
        else
        {
            Debug.Log("GameController | DelayStart: Game resumed.");
            IntermissionEnded?.Invoke();
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("GameController | HandleGameOver: Game over.");
        _gameInProgress = false;
        GameEnded?.Invoke();
    }

    /* Getters and Setters */

    public bool IsGameInProgress()
    {
        return _gameInProgress;
    }
}
