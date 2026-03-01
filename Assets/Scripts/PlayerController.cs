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


        // Handle player movement.

    }
}
