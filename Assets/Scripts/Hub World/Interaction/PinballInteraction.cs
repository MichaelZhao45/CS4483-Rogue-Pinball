using UnityEngine;
using UnityEngine.InputSystem;

public class PinballInteraction : InteractionBase
{
    [SerializeField] private PinballGameController _pinballControl;

    [Header("Actions")]
    [SerializeField] private InputActionReference _startGameAction;

    void Awake()
    {
        _startGameAction.action.Enable();
        _startGameAction.action.performed += StartPinballGame;
    }

    void OnDestroy()
    {
        _startGameAction.action.Disable();
        _startGameAction.action.performed -= StartPinballGame;
    }

    void StartPinballGame(InputAction.CallbackContext context)
    {
        if (pc != null && playerNearby)
        {
            pc.ClearInteractionText();
            pc.SwitchCameras();

            StartCoroutine(_pinballControl.DelayStartGame(2.0f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
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