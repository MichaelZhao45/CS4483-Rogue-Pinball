using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed = 3.0f;

    private Vector2 _moveInput;

    private CharacterController cc;
    private PlayerInput playerInput;

    [Header("Cameras")]
    [SerializeField] private CinemachineCamera _FPCamera;
    [SerializeField] private CinemachineCamera _zoomCamera;
    private bool _isZoomed = false;

    [Header("HUD Settings")]
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private Canvas HUD;

    // Audio Settings
    private AudioSource _audioSource;
    private bool _isFootstepsPlaying = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
        playerInput = GetComponent<PlayerInput>();

        _isZoomed = false;

        // Handling the action maps.
        DisablePinballMode();
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>().normalized;
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
        if (_moveInput.magnitude >= 0.1f && !_isZoomed)
        {
            if (!_isFootstepsPlaying)
            {
                _audioSource.Play();
                _isFootstepsPlaying = true;
            }

            if (!_isZoomed)
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
        _isZoomed = !_isZoomed;

        _zoomCamera.gameObject.SetActive(_isZoomed);
        _FPCamera.gameObject.SetActive(!_isZoomed);

        if (_isZoomed)
        {
            _zoomCamera.Prioritize();
        }
        else
        {
            _FPCamera.Prioritize();
        }
    }

    public void ShowInteractionText(string interactionText)
    {
        _interactionText.text = interactionText;
    }

    public void ClearInteractionText()
    {
        _interactionText.text = "";
    }

    public void EnablePinballMode()
    {
        playerInput.actions.FindActionMap("Pinball").Enable();
        playerInput.actions.FindActionMap("Hub World").Disable();
        TurnOffHUD();
    }

    public void DisablePinballMode()
    {
        playerInput.actions.FindActionMap("Pinball").Disable();
        playerInput.actions.FindActionMap("Hub World").Enable();
        TurnOnHUD();
    }

    public void TurnOffHUD()
    {
        HUD.enabled = false;
    }

    public void TurnOnHUD()
    {
        HUD.enabled = true;
    }
}
