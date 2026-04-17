using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Canvas _shopCanvas;
    [SerializeField] private PlayerController player;
    [SerializeField] private InventoryManager playerInventory;
    [SerializeField] private PowerUp[] availablePowerUps;
    [SerializeField] private UIManager UI;
    private PowerUp[] _shopOptions = new PowerUp[3];

    // TODO: refactor this to not be an event; only AudioController cares.
    public static event Action ShopOpened;

    private void OnEnable()
    {
        RoundManager.RoundOver += InitializeShop;
    }

    private void OnDisable()
    {
        RoundManager.RoundOver -= InitializeShop;
    }

    public void Awake()
    {
        InitializeShop();
    }

    public void InitializeShop()
    {
        System.Random rng = new();

        _shopOptions[0] = availablePowerUps[rng.Next(0, availablePowerUps.Length)];
        _shopOptions[1] = availablePowerUps[rng.Next(0, availablePowerUps.Length)];
        _shopOptions[2] = availablePowerUps[rng.Next(0, availablePowerUps.Length)];

        UI.SetShop(_shopOptions);
    }

    public void Show()
    {
        _shopCanvas.gameObject.SetActive(true);
        ShopOpened?.Invoke();
        player.TurnOffHUD();

        player.EnterShopMode();
    }

    public void Hide()
    {
        _shopCanvas.gameObject.SetActive(false);
        player.TurnOnHUD();

        player.ExitShopMode();
    }

    public void PurchaseItem(int index)
    {
        PowerUp purchasedPowerUp = _shopOptions[index];

        if (playerInventory.GetTokens() >= purchasedPowerUp.cost)
        {
            Debug.Log("[SHOP] Item purchased successfully.");

            playerInventory.SubtractTokens(purchasedPowerUp.cost);
            UI.SetTokens(playerInventory.GetTokens());

            playerInventory.AddPowerUp(purchasedPowerUp);
        }
        else
        {
            Debug.Log("[SHOP] Cannot purchase item!");
        }
    }
}
