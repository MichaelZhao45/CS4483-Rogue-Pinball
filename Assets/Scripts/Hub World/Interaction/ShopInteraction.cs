using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInteraction : InteractionBase
{
    [SerializeField] private Shop shop;

    public void OpenShop(InputAction.CallbackContext context)
    {
        if (player != null && playerNearby)
        {
            if (context.performed)
            {
                shop.Show();
                player.ClearInteractionText();
            }
        }
    }
}
