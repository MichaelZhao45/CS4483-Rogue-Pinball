using UnityEngine;
using UnityEngine.InputSystem;

public class PinballInteraction : InteractionBase
{
    [SerializeField] private GameController _gameControl;
    [SerializeField] private float _gameStartDelay = 2.0f;
    [SerializeField] private AudioSource _ambience;

    public void StartGame(InputAction.CallbackContext context)
    {
        if (player != null && playerNearby)
        {
            if (context.performed)
            {
                player.ClearInteractionText();
                player.SwitchCameras();
                    
                _ambience.Stop();
                StartCoroutine(_gameControl.DelayStartGame(_gameStartDelay));

                // Change the player's active action map to the pinball mode.
                player.EnablePinballMode();
            }
        }
    }

    public void QuitGame()
    {
        if (player != null && playerNearby)
        {
            player.SwitchCameras();

            _ambience.Play();

            // Change the player's active action map to the hub world mode.
            player.DisablePinballMode();
        }
    }
}