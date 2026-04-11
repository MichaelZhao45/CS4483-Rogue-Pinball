using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PinballInteraction : InteractionBase
{
    [SerializeField] private GameController _gameControl;
    [SerializeField] private float _gameStartDelay = 2.0f;
    [SerializeField] private AudioSource _ambience;

    public Image fadeCover;

    private void FadeCameraTransition()
    {
        Sequence fadeSequence = DOTween.Sequence();
        fadeSequence.Append(fadeCover.DOFade(1f, 1f));
        fadeSequence.AppendCallback(player.SwitchCameras);
        fadeSequence.AppendInterval(1f);
        fadeSequence.Append(fadeCover.DOFade(0f, 1f));
        fadeSequence.Play();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (player != null && playerNearby)
        {
            if (context.performed)
            {
                player.ClearInteractionText();

                FadeCameraTransition();
                    
                _ambience.Stop();

                // If the game has already begun, it will end the intermission and resume the game.
                // Otherwise, it will start a new game.
                StartCoroutine(_gameControl.DelayStart(_gameStartDelay));

                // Change the player's active action map to the pinball mode.
                player.EnablePinballMode();
            }
        }
    }

    public void QuitGame()
    {
        if (player != null && playerNearby)
        {
            FadeCameraTransition();

            _ambience.Play();

            // Change the player's active action map to the hub world mode.
            player.DisablePinballMode();
        }
    }
}