using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperController : MonoBehaviour
{
    public float restPosition = 0f;
    public float pressedPosition = -45f;
    public float hitStrength = 80000f;
    public float releaseStrength = 10000f;
    public float dampening = 100f;
    public HingeJoint LFlipperHinge;
    public HingeJoint RFlipperHinge;
    public InputActionReference LFlipperAction;
    public InputActionReference RFlipperAction;

    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();

    private bool _leftFlipperPressed, _rightFlipperPressed;

    private void Awake()
    {
        LFlipperAction.action.Enable();
        RFlipperAction.action.Enable();

        LFlipperAction.action.performed += LeftFlipperPressed;
        RFlipperAction.action.performed += RightFlipperPressed;

        LFlipperAction.action.canceled += LeftFlipperReleased;
        RFlipperAction.action.canceled += RightFlipperReleased;
    }

    private void OnDestroy()
    {
        LFlipperAction.action.Disable();
        RFlipperAction.action.Disable();

        LFlipperAction.action.performed -= LeftFlipperPressed;
        RFlipperAction.action.performed -= RightFlipperPressed;

        LFlipperAction.action.canceled -= LeftFlipperReleased;
        RFlipperAction.action.canceled -= RightFlipperReleased;
    }

    void Start()
    {
        // Initializing springs.
        jointSpringPressed.spring = hitStrength;
        jointSpringReleased.spring = releaseStrength;
        jointSpringPressed.damper = jointSpringReleased.damper = dampening;
        jointSpringPressed.targetPosition = LFlipperHinge.limits.max;
        jointSpringReleased.targetPosition = LFlipperHinge.limits.min;
    }

    void Update()
    {
        // Controlling left flipper.
        if (_leftFlipperPressed) LFlipperHinge.spring = jointSpringPressed;
        else LFlipperHinge.spring = jointSpringReleased;
        // Controlling right flipper.
        if (_rightFlipperPressed) RFlipperHinge.spring = jointSpringPressed;
        else RFlipperHinge.spring = jointSpringReleased;
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
}
