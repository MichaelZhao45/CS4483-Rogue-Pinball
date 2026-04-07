using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class LightSwitchInteraction : InteractionBase
{
    [SerializeField] private GameObject _light;
    private FlickerLight _fl;
    private AudioSource _audioSource;

    protected override void Start()
    {
        base.Start();
        _fl = _light.GetComponent<FlickerLight>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (player != null && _fl != null && playerNearby)
        {
            if (context.performed)
            {
                _fl.ToggleOnOff();
                _audioSource.Play();
            }
        }
    }
}
