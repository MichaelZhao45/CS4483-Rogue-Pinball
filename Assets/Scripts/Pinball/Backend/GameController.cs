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

    /* Orchestrator events. Controls the flow of gameplay states. */

    // Signals that a new game has been started.
    public static event Action GameStarted;
    // Signals that the game is over after all balls have been lost.
    public static event Action GameOver;
    // Signals that the game has been started again after the intermission period.
    public static event Action GameContinued;

    /* Event Subscriptions */

    private void OnEnable()
    {
        Drain.OnDrainHit += OnBallDrained;

        RoundManager.RoundStart += OnRoundStart;
    }

    private void OnDisable()
    {
        Drain.OnDrainHit -= OnBallDrained;

        RoundManager.RoundStart -= OnRoundStart;
    }

    /* Event Reactions */

    private void OnRoundStart()
    {
        // Reset the ball counter back to full for the next round.
        _ballsRemaining = _maxBalls;
        UI.SetBalls(_ballsRemaining);
    }

    private void OnBallDrained()
    {
        _ballsRemaining--;
        UI.SetBalls(_ballsRemaining);
        
        if (_ballsRemaining >= 0) dropper.ActivateDropper();
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
            RestartGame();
        }
        // If a game is already in progress, resume the game.
        else
        {
            Debug.Log("GameController | DelayStart: Game continued.");
            GameContinued?.Invoke();
        }
    }

    public void RestartGame()
    {
        _gameInProgress = true;
        GameStarted?.Invoke();
    }

    private void HandleGameOver()
    {
        Debug.Log("GameController | HandleGameOver: Game over.");
        _gameInProgress = false;
        GameOver?.Invoke();
    }

    /* Getters and Setters */

    public bool IsGameInProgress()
    {
        return _gameInProgress;
    }

    public int getBallsRemaining()
    {
        return _ballsRemaining;
    }
}
