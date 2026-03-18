using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed = 3.0f;
    private bool _isMovementLocked = false;

    private Vector2 _moveInput;

    private CharacterController cc;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineCamera _FPCamera;
    [SerializeField] private CinemachineCamera _zoomCamera;
    private bool _isZoomed = false;

    [Header("HUD Settings")]
    [SerializeField] private Canvas _hudCanvas;
    [SerializeField] private TMP_Text _interactionText;

    [Header("Interaction Prompts")]
    [SerializeField] private string _pinballInteractionText;
    [SerializeField] private string _pinballNarrativeInteractionText;
    [SerializeField] private string _bedInteractionText;
    [SerializeField] private string _lightSwitchInteractionText;

    // Audio Settings
    private AudioSource _audioSource;
    private bool _isFootstepsPlaying = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        _isZoomed = false;

        _audioSource = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>().normalized;
    }

    void Update()
    {
        // Rotate player to match camera rotation.
        Vector3 cameraForward = _FPCamera.transform.forward.normalized;
        cameraForward.y = 0f; // Ignore the y-axis rotation.
        if (cameraForward != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = newRotation;
        }


        // Handle player movement
        if (_moveInput.magnitude >= 0.1f && !_isMovementLocked)
        {
            if (!_isFootstepsPlaying)
            {
                _audioSource.Play();
                _isFootstepsPlaying = true;
            }

            if (!_isMovementLocked)
            {
                Vector3 movement = (transform.right * _moveInput.x) + (transform.forward * _moveInput.y);
                cc.Move(_moveSpeed * Time.deltaTime * movement);
            }
        }
        else
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
                _isFootstepsPlaying = false;
            }
        }
    }

    public void SwitchCameras()
    {
        ToggleMovementLock();

        if (!_isZoomed)
        {
            _zoomCamera.gameObject.SetActive(true);
            _zoomCamera.Prioritize();
        }
        else
        {
            _zoomCamera.gameObject.SetActive(false);
            _FPCamera.Prioritize();
        }

        _isZoomed = !_isZoomed;
    }

    public void ShowPinballInteractionPrompt()
    {
        _interactionText.text = _pinballInteractionText;
    }

    public void ShowPinballNarrativeInteractionText()
    {
        _interactionText.text = _pinballNarrativeInteractionText;
    }

    public void ShowBedInteractionText()
    {
        _interactionText.text = _bedInteractionText;
    }

    public void ShowLightSwitchInteractionText()
    {
        _interactionText.text = _lightSwitchInteractionText;
    }

    public void ClearInteractionText()
    {
        _interactionText.text = "";
    }

    public void ToggleMovementLock()
    {
        _isMovementLocked = !_isMovementLocked;
    }
}
