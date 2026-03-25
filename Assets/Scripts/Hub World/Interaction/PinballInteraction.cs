using UnityEngine;
using UnityEngine.InputSystem;

public class PinballInteraction : InteractionBase
{
    [SerializeField] private PinballGameController _pinballControl;
    [SerializeField] private float _gameStartDelay = 2.0f;
    private bool _gamePlaying = false;

    public void StartGame(InputAction.CallbackContext context)
    {
        if (player != null && playerNearby)
        {
            if (context.performed)
            {
                player.ClearInteractionText();
                player.SwitchCameras();

                if (!_gamePlaying)
                {
                    StartCoroutine(_pinballControl.DelayStartGame(_gameStartDelay));
                }

                _gamePlaying = !_gamePlaying;
            }
        }
    }
}