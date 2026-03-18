using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitchInteraction : InteractionBase
{
    [SerializeField] private GameObject _light;
    private FlickerLight _fl;
    private AudioSource _audioSource;

    [Header("Actions")]
    [SerializeField] private InputActionReference _interactAction;

    void Awake()
    {
        _interactAction.action.Enable();
        _interactAction.action.performed += ToggleLightSwitch;

        _fl = _light.GetComponent<FlickerLight>();

        _audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        _interactAction.action.Disable();
        _interactAction.action.performed -= ToggleLightSwitch;
    }

    void ToggleLightSwitch(InputAction.CallbackContext context)
    {
        if (pc != null && _fl != null && playerNearby)
        {
            _fl.ToggleOnOff();
            _audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            pc.ShowLightSwitchInteractionText();
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
