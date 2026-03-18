using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinballGameController : MonoBehaviour
{
    public InputActionReference ReleaseBallAction;

    [Header("Game Control")]
    private bool _gameStarted;
    private bool _ballDropped;
    private static int _currentScore;
    private int _scoreThreshold;

    // Ball Drop Controls
    [SerializeField] private float _ballDropMoveLength = 5.0f;
    [SerializeField] private float _ballDropMoveSpeed = 0.1f;
    private bool _isBallDropMoveRight;
    private float _ballDropLeftBound, _ballDropRightBound;

    [Header("Game Elements")]
    [SerializeField] private GameObject _ball;
    private Rigidbody _ballRb;

    [Header("UI Elements")]
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Canvas _UICanvas;
    [SerializeField] private TMP_Text _roundCounterText;
    [SerializeField] private TMP_Text _scoreCounterText;
    [SerializeField] private TMP_Text _scoreThresholdText;

    private void OnEnable()
    {
        ReleaseBallAction.action.Enable();
        ReleaseBallAction.action.performed += DropBall;

        Bumper.OnBumperHit += AddScore;
    }

    private void OnDisable()
    {
        ReleaseBallAction.action.Disable();
        ReleaseBallAction.action.performed -= DropBall;

        Bumper.OnBumperHit -= AddScore;
    }

    void Start()
    {
        // Initializing game control.
        _gameStarted = false;
        _ballDropped = false;

        // Setting up ball drop.
        _ballDropLeftBound = _ball.transform.position.x - _ballDropMoveLength;
        _ballDropRightBound = _ball.transform.position.x + _ballDropMoveLength;
        _isBallDropMoveRight = true;

        // Initializing game elements.
        _ballRb = _ball.GetComponent<Rigidbody>();
        _ballRb.useGravity = false;
        _ballRb.isKinematic = true;

        // Initializing score system.
        _currentScore = 0;
    }

    void FixedUpdate()
    {
        // Prevent game logic from running when the player has not yet started the game.
        if (!_gameStarted || _ballDropped) return;

        Vector3 moveDirection = _isBallDropMoveRight ? Vector3.right : Vector3.left;
        moveDirection *= _ballDropMoveSpeed * Time.fixedDeltaTime;

        // Moving the ball at the start of the game.
        if (!_ballDropped)
        {
            _ballRb.MovePosition(_ball.transform.position + moveDirection);

            if (!_isBallDropMoveRight && _ball.transform.position.x <= _ballDropLeftBound
                || _isBallDropMoveRight && _ball.transform.position.x >= _ballDropRightBound)
            {
                _isBallDropMoveRight = !_isBallDropMoveRight;
            }
        }
    }

    public void DropBall(InputAction.CallbackContext context)
    {
        _ballDropped = true;
        
        _ballRb.useGravity = true;
        _ballRb.isKinematic = false;
    }

    public IEnumerator DelayStartGame(float time)
    {
        yield return new WaitForSeconds(time);
        _gameStarted = true;
        _UICanvas.gameObject.SetActive(true);
    }

    private void AddScore(Bumper bumper)
    {
        _currentScore += bumper.GetPoints();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _scoreCounterText.text = _currentScore.ToString();
    }
}
