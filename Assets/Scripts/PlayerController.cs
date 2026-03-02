using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed = 3.0f;

    private Vector2 _moveInput;

    private CharacterController cc;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineCamera _FPCamera;
    [SerializeField] private CinemachineCamera _zoomCamera;
    private bool _isZoomed = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        _isZoomed = false;
    }

    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>().normalized;
    }

    void OnInteract()
    {
        SwitchCameras();
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
        if (_moveInput.magnitude >= 0.1f)
        {
            Vector3 movement = (transform.right * _moveInput.x) + (transform.forward * _moveInput.y);
            cc.Move(_moveSpeed * Time.deltaTime * movement);
        }
    }

    private void SwitchCameras()
    {
        if (!_isZoomed)
        {
            _zoomCamera.Prioritize();
        }
        else
        {
            _FPCamera.Prioritize();
        }

        _isZoomed = !_isZoomed;
    }
}
