using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallDropper : MonoBehaviour
{
    public BallManager ballManager;

    [SerializeField] private GameObject _dropperAnchor;
    private Vector3 _dropperStartPostion;

    [SerializeField] private float _ballDropMoveLength = 5.0f;
    [SerializeField] private float _ballDropMoveSpeed = 0.1f;
    private bool _isBallDropMoveRight;
    private float _ballDropLeftBound, _ballDropRightBound;
    private bool _isActive;

    public static event Action BallDropped;

    void OnEnable()
    {
        RoundManager.RoundStart += ActivateDropper;
        RoundManager.RoundOver += DeactivateDropper;
    }

    void OnDisable()
    {
        RoundManager.RoundStart -= ActivateDropper;
        RoundManager.RoundOver -= DeactivateDropper;
    }

    void Start()
    {
        _dropperStartPostion = _dropperAnchor.transform.position;

        // Defining the bounds of the dropper's movement.
        float dropperX = _dropperAnchor.transform.position.x;
        _ballDropLeftBound = dropperX - _ballDropMoveLength;
        _ballDropRightBound = dropperX + _ballDropMoveLength;
        _isBallDropMoveRight = true;

        // The dropper should not move until intentionally made active by GameController.
        _isActive = false;
    }

    void FixedUpdate()
    {
        // Prevent game logic from running when the player has not yet started the game.
        if (!_isActive) return;

        Vector3 moveDirection = _isBallDropMoveRight ? Vector3.right : Vector3.left;
        moveDirection *= _ballDropMoveSpeed * Time.fixedDeltaTime;

        // Moving the ball.
        _dropperAnchor.transform.position += moveDirection;

        if (!_isBallDropMoveRight && _dropperAnchor.transform.position.x <= _ballDropLeftBound
            || _isBallDropMoveRight && _dropperAnchor.transform.position.x >= _ballDropRightBound)
        {
            _isBallDropMoveRight = !_isBallDropMoveRight;
        }
    }

    public void ActivateDropper(int round = 0)
    {
        _isActive = true;
        _dropperAnchor.SetActive(true);
    }

    public void DeactivateDropper()
    {
        _isActive = false;
        _dropperAnchor.SetActive(false);
    }

    public void ReleaseBall(InputAction.CallbackContext context)
    {
        if (!_isActive) return;

        DeactivateDropper();
        ballManager.SpawnBall(_dropperAnchor.transform.position);
        _dropperAnchor.SetActive(false);

        BallDropped?.Invoke();
    }
}
