using UnityEngine;
using UnityEngine.InputSystem;

public class BallDropper : MonoBehaviour
{
    public BallManager ballManager;

    [SerializeField] private GameObject _dropperAnchor;

    [SerializeField] private float _ballDropMoveLength = 5.0f;
    [SerializeField] private float _ballDropMoveSpeed = 0.1f;
    private bool _isBallDropMoveRight;
    private float _ballDropLeftBound, _ballDropRightBound;
    private bool _ballDropped;
    private bool _isActive;

    /*
    void OnEnable()
    {
        Drain.OnDrainHit += Restart;
    }

    void OnDisable()
    {
        Drain.OnDrainHit -= Restart;
    }
    */

    void Start()
    {
        float dropperX = _dropperAnchor.transform.position.x;

        _ballDropLeftBound = dropperX - _ballDropMoveLength;
        _ballDropRightBound = dropperX + _ballDropMoveLength;
        _isBallDropMoveRight = true;

        _isActive = false;
    }

    void FixedUpdate()
    {
        // Prevent game logic from running when the player has not yet started the game.
        if (!_isActive) return;

        Vector3 moveDirection = _isBallDropMoveRight ? Vector3.right : Vector3.left;
        moveDirection *= _ballDropMoveSpeed * Time.fixedDeltaTime;

        // Moving the ball at the start of the game.
        if (!_ballDropped)
        {
            //_startingBallRb.MovePosition(_startingBallInstance.transform.position + moveDirection);

            _dropperAnchor.transform.position += moveDirection;

            if (!_isBallDropMoveRight && _dropperAnchor.transform.position.x <= _ballDropLeftBound
                || _isBallDropMoveRight && _dropperAnchor.transform.position.x >= _ballDropRightBound)
            {
                _isBallDropMoveRight = !_isBallDropMoveRight;
            }
        }
    }

    public void SetDropperActive(bool state)
    {
        _isActive = state;
    }

    public void Restart()
    {
        _dropperAnchor.SetActive(true);
        _ballDropped = false;
        SetDropperActive(true);
    }

    public void ReleaseBall(InputAction.CallbackContext context)
    {
        if (_ballDropped) return;

        _ballDropped = true;
        SetDropperActive(false);
        ballManager.SpawnBall(_dropperAnchor.transform.position);
        _dropperAnchor.SetActive(false);
    }
}
