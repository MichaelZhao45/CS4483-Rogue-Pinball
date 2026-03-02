using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _moveSpeed = 3.0f;

    private Vector2 _moveInput;

    private CharacterController cc;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineCamera _camera;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>().normalized;
    }

    void Update()
    {
        // Rotate player to match camera rotation.
        Vector3 cameraForward = _camera.transform.forward.normalized;
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
}
