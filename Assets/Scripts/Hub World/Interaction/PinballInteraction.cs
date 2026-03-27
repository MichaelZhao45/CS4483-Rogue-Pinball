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

                if (!_gameControl.gameInProgress)
                {
                    _ambience.Stop();
                    StartCoroutine(_gameControl.DelayStartGame(_gameStartDelay));
                }
                else
                {
                    _ambience.Play();
                }
            }
        }
    }
}