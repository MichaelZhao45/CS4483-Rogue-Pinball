using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ExitDoorInteraction : InteractionBase
{
    public Image fadeCover;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (player != null && playerNearby)
        {
            if (context.performed)
            {
                Sequence fadeSequence = DOTween.Sequence();
                fadeSequence.AppendCallback(_audioSource.Play);
                fadeSequence.Append(fadeCover.DOFade(1f, 2f));
                fadeSequence.AppendCallback(Application.Quit);
                fadeSequence.Play();
            }
        }
    }
}
