using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinballGameController : MonoBehaviour
{
    [Header("Game Control")]
    private bool _gameStarted;
    private bool _startingBallDropped;
    private static int _currentScore;
    private int _scoreThreshold;
    private int _currentRound;
    private int _ballsRemaining;

    // Ball Drop Controls
    [SerializeField] private float _ballDropMoveLength = 5.0f;
    [SerializeField] private float _ballDropMoveSpeed = 0.1f;
    private bool _isBallDropMoveRight;
    private float _ballDropLeftBound, _ballDropRightBound;

    [Header("Ball")]
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballStart;
    private GameObject _startingBallInstance;
    private Rigidbody _startingBallRb;

    [Header("UI Elements")]
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Canvas _UICanvas;
    [SerializeField] private TMP_Text _roundCounterText;
    [SerializeField] private TMP_Text _scoreCounterText;
    [SerializeField] private TMP_Text _scoreThresholdText;
    [SerializeField] private TMP_Text _ballsRemainingText;
    private bool _UIEnabled;

    private void OnEnable()
    {
        Bumper.OnBumperHit += AddScore;
        Drain.OnDrainHit += OnBallDrained;
    }

    private void OnDisable()
    {
        Bumper.OnBumperHit -= AddScore;
        Drain.OnDrainHit -= OnBallDrained;
    }

    void Start()
    {
        // Setting up ball drop.
        _ballDropLeftBound = _ballStart.transform.position.x - _ballDropMoveLength;
        _ballDropRightBound = _ballStart.transform.position.x + _ballDropMoveLength;
        _isBallDropMoveRight = true;

        // Initializing UI elements.
        _scoreThresholdText.text = _scoreThreshold.ToString();
        _roundCounterText.text = _currentRound.ToString();
        _UIEnabled = false;
    }

    void Update()
    {
        if (_currentScore >= _scoreThreshold)
        {
            IncrementRound();
        }
    }

    void FixedUpdate()
    {
        // Prevent game logic from running when the player has not yet started the game.
        if (!_gameStarted || _startingBallDropped) return;

        Vector3 moveDirection = _isBallDropMoveRight ? Vector3.right : Vector3.left;
        moveDirection *= _ballDropMoveSpeed * Time.fixedDeltaTime;

        // Moving the ball at the start of the game.
        if (!_startingBallDropped)
        {
            _startingBallRb.MovePosition(_startingBallInstance.transform.position + moveDirection);

            if (!_isBallDropMoveRight && _startingBallInstance.transform.position.x <= _ballDropLeftBound
                || _isBallDropMoveRight && _startingBallInstance.transform.position.x >= _ballDropRightBound)
            {
                _isBallDropMoveRight = !_isBallDropMoveRight;
            }
        }
    }

    public void OnLaunch(InputAction.CallbackContext context)
    {
        if (_startingBallDropped) return;

        _startingBallDropped = true;

        _startingBallRb.useGravity = true;
        _startingBallRb.isKinematic = false;
    }

    public IEnumerator DelayStartGame(float time)
    {
        yield return new WaitForSeconds(time);

        InitializeBoard();
        InitializeGame();

        TogglePinballUI();

        _gameStarted = true;
    }

    private void InitializeBoard()
    {
        _startingBallInstance = Instantiate(_ballPrefab, _ballStart.position, _ballStart.rotation);

        _startingBallRb = _startingBallInstance.GetComponent<Rigidbody>();
        _startingBallRb.useGravity = false;
        _startingBallRb.isKinematic = true;

        // Restart the ball drop.
        _startingBallDropped = false;
    }

    private void InitializeGame()
    {
        // Initializing game control.
        _gameStarted = false;
        _startingBallDropped = false;

        // Initializing score and round system.
        _currentScore = 0;
        _scoreThreshold = 250;
        _currentRound = 1;
        _ballsRemaining = 3;

        UpdateBallsRemainingUI();
    }

    private void OnBallDrained(Drain drain)
    {
        _ballsRemaining--;

        UpdateBallsRemainingUI();
        
        if (_ballsRemaining > 0)
        {
            InitializeBoard();
        }
        else
        {
            Debug.Log("Game Over!");
        }
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

    private void UpdateBallsRemainingUI()
    {
        _ballsRemainingText.text = _ballsRemaining.ToString();
    }

    private void IncrementRound()
    {
        _currentScore = 0;
        UpdateScoreUI();

        _currentRound++;
        _scoreThreshold += 250;

        _scoreThresholdText.text = _scoreThreshold.ToString();
        _roundCounterText.text = _currentRound.ToString();
    }

    public void TogglePinballUI()
    {
        _UIEnabled = !_UIEnabled;
        _UICanvas.gameObject.SetActive(_UIEnabled);
    }
}
