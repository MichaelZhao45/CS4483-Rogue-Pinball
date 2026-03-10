using UnityEngine;
using UnityEngine.InputSystem;

public class PinballInteraction : MonoBehaviour
{
    private bool playerNearby = false;
    private PlayerController pc;

    [Header("Actions")]
    [SerializeField] private InputActionReference _startGameAction;

    void Awake()
    {
        _startGameAction.action.Enable();
        _startGameAction.action.performed += TransitionCameras;
    }

    void OnDestroy()
    {
        _startGameAction.action.Disable();
        _startGameAction.action.performed -= TransitionCameras;
    }

    void TransitionCameras(InputAction.CallbackContext context)
    {
        if (pc != null && playerNearby)
        {
            pc.ClearInteractionText();
            pc.SwitchCameras();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            pc = other.gameObject.GetComponent<PlayerController>();
            pc.ShowPinballInteractionPrompt();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            pc.ClearInteractionText();
        }
    }
}