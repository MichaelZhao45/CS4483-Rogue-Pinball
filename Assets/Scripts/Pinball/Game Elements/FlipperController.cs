using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperController : MonoBehaviour
{
    //public float restPosition = 0f;
    //public float pressedPosition = -45f;
    public float hitStrength = 80000f;
    public float releaseStrength = 10000f;
    public float dampening = 100f;
    public HingeJoint LFlipperHinge;
    public HingeJoint RFlipperHinge;

    private JointSpring jointSpringReleased = new();
    private JointSpring jointSpringPressed = new();

    [SerializeField] private AudioSource _flipperAudioSource;
    [SerializeField] private AudioClip _flipperSFX;

    void Start()
    {
        // Initializing springs.
        jointSpringPressed.spring = hitStrength;
        jointSpringReleased.spring = releaseStrength;
        jointSpringPressed.damper = jointSpringReleased.damper = dampening;
        jointSpringPressed.targetPosition = LFlipperHinge.limits.max;
        jointSpringReleased.targetPosition = LFlipperHinge.limits.min;
    }

    public void RightFlipper(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _flipperAudioSource.PlayOneShot(_flipperSFX);
            RFlipperHinge.spring = jointSpringPressed;
        }
        else if (context.canceled)
        {
            RFlipperHinge.spring = jointSpringReleased;
        }
    }

    public void LeftFlipper(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _flipperAudioSource.PlayOneShot(_flipperSFX);
            LFlipperHinge.spring = jointSpringPressed;
        }
        else if (context.canceled)
        {
            LFlipperHinge.spring = jointSpringReleased;
        }
    }
}
