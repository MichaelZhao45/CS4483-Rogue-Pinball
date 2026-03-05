using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinballController : MonoBehaviour
{
    public float restPosition = 0f;
    public float pressedPosition = -45f;
    public float hitStrength = 80000f;
    public float releaseStrength = 10000f;
    public float dampening = 100f;
    public HingeJoint LeftHinge;
    public HingeJoint RightHinge;
    public InputActionReference LFlipper;
    public InputActionReference RFlipper;
    public InputActionReference Plunger;

    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();

    private bool leftFlipperPressed, rightFlipperPressed;

    private void Awake()
    {
        LFlipper.action.Enable();
        RFlipper.action.Enable();
        Plunger.action.Enable();

        LFlipper.action.performed += LeftFlipper;
        RFlipper.action.performed += RightFlipper;
        Plunger.action.performed += Launch;

        LFlipper.action.canceled += LeftFlipperReleased;
        RFlipper.action.canceled += RightFlipperReleased;
        Plunger.action.canceled += LaunchReleased;
    }

    void Start()
    {
        jointSpringPressed.spring = hitStrength;
        jointSpringReleased.spring = releaseStrength;
        jointSpringPressed.damper = jointSpringReleased.damper = dampening;
        jointSpringPressed.targetPosition = LeftHinge.limits.max;
        jointSpringReleased.targetPosition = LeftHinge.limits.min;
    }

    // Update is called once per frame
    void Update()
    {
        if(leftFlipperPressed)
        {
            LeftHinge.spring = jointSpringPressed;
        }
        else
        {
            LeftHinge.spring = jointSpringReleased;
        }

        if(rightFlipperPressed)
        {
            RightHinge.spring = jointSpringPressed;
        } 
        else
        {
            RightHinge.spring = jointSpringReleased;
        }
    }
    private void OnDestroy()
    {
        LFlipper.action.Disable();
        RFlipper.action.Disable();
        Plunger.action.Disable();

        LFlipper.action.performed -= LeftFlipper;
        RFlipper.action.performed -= RightFlipper;
        Plunger.action.performed -= Launch;

        LFlipper.action.canceled -= LeftFlipperReleased;
        RFlipper.action.canceled -= RightFlipperReleased;
        Plunger.action.canceled -= LaunchReleased;
    }

    public void RightFlipper(InputAction.CallbackContext context)
    {
        rightFlipperPressed = true;
    }

    public void RightFlipperReleased(InputAction.CallbackContext context)
    {
        rightFlipperPressed = false;
    }

    public void LeftFlipper(InputAction.CallbackContext context)
    {
        leftFlipperPressed = true;
    }

    public void LeftFlipperReleased(InputAction.CallbackContext context)
    {
        leftFlipperPressed = false;
    }

    public void Launch(InputAction.CallbackContext context)
    {

    }

    public void LaunchReleased(InputAction.CallbackContext context)
    {

    }
}
