using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinballController : MonoBehaviour
{
    [Header("Game Control")]
    private bool _gameStarted;
    private bool _ballDropped;

    // Ball Drop Controls
    [SerializeField] private float _ballDropMoveLength = 5.0f;
    [SerializeField] private float _ballDropMoveSpeed = 0.1f;
    private bool _isBallDropMoveRight;
    private float _ballDropLeftBound, _ballDropRightBound;

    [Header("Flippers")]
    public float restPosition = 0f;
    public float pressedPosition = -45f;
    public float hitStrength = 80000f;
    public float releaseStrength = 10000f;
    public float dampening = 100f;
    public HingeJoint LFlipperHinge;
    public HingeJoint RFlipperHinge;
    public InputActionReference LFlipperAction;
    public InputActionReference RFlipperAction;
    public InputActionReference ReleaseBallAction;

    [Header("Game Elements")]
    [SerializeField] private GameObject _ball;
    private Rigidbody _ballRb;

    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();

    private bool _leftFlipperPressed, _rightFlipperPressed;

    private void Awake()
    {
        LFlipperAction.action.Enable();
        RFlipperAction.action.Enable();
        ReleaseBallAction.action.Enable();

        LFlipperAction.action.performed += LeftFlipperPressed;
        RFlipperAction.action.performed += RightFlipperPressed;
        ReleaseBallAction.action.performed += DropBall;

        LFlipperAction.action.canceled += LeftFlipperReleased;
        RFlipperAction.action.canceled += RightFlipperReleased;
    }

    void Start()
    {
        // Initializing springs.
        jointSpringPressed.spring = hitStrength;
        jointSpringReleased.spring = releaseStrength;
        jointSpringPressed.damper = jointSpringReleased.damper = dampening;
        jointSpringPressed.targetPosition = LFlipperHinge.limits.max;
        jointSpringReleased.targetPosition = LFlipperHinge.limits.min;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent game logic from running when the player has not yet started the game.
        if (!_gameStarted) return;

        // Controlling left flipper.
        if (_leftFlipperPressed) LFlipperHinge.spring = jointSpringPressed;
        else LFlipperHinge.spring = jointSpringReleased;
        // Controlling right flipper.
        if (_rightFlipperPressed) RFlipperHinge.spring = jointSpringPressed;
        else RFlipperHinge.spring = jointSpringReleased;
    }

    void FixedUpdate()
    {
        // Prevent game logic from running when the player has not yet started the game.
        if (!_gameStarted) return;

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

    private void OnDestroy()
    {
        LFlipperAction.action.Disable();
        RFlipperAction.action.Disable();
        ReleaseBallAction.action.Disable();

        LFlipperAction.action.performed -= LeftFlipperPressed;
        RFlipperAction.action.performed -= RightFlipperPressed;
        ReleaseBallAction.action.performed -= DropBall;

        LFlipperAction.action.canceled -= LeftFlipperReleased;
        RFlipperAction.action.canceled -= RightFlipperReleased;
    }

    public void RightFlipperPressed(InputAction.CallbackContext context)
    {
        _rightFlipperPressed = true;
    }

    public void RightFlipperReleased(InputAction.CallbackContext context)
    {
        _rightFlipperPressed = false;
    }

    public void LeftFlipperPressed(InputAction.CallbackContext context)
    {
        _leftFlipperPressed = true;
    }

    public void LeftFlipperReleased(InputAction.CallbackContext context)
    {
        _leftFlipperPressed = false;
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

        Debug.Log("Game has started!");
    }
}
