using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Canvas _shopCanvas;
    [SerializeField] private PlayerController player;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private GameObject AvailablePowerUps;
    [SerializeField] private UIManager UI;
    [SerializeField] private GameObject[] shopOptions;

    // TODO: refactor this to not be an event; only AudioController cares.
    public static event Action ShopOpened;

    public void Awake()
    {
        InitializeShop();
    }

    public void InitializeShop()
    {
        shopOptions = new GameObject[3];
        shopOptions[0] = AvailablePowerUps.transform.GetChild(0).gameObject;
        shopOptions[1] = AvailablePowerUps.transform.GetChild(0).gameObject;
        shopOptions[2] = AvailablePowerUps.transform.GetChild(0).gameObject;

        UI.SetShop(shopOptions);
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

    public void purchaseItem1()
    {
        Debug.Log($"[SHOP] Purchase button clicked. Sending item to: {playerInventory.gameObject.name}");
        PowerUp slot1 = shopOptions[0].GetComponent<PowerUp>();
        if (playerInventory.GetTokens() >= slot1.getCost())
        {
            playerInventory.SubtractTokens(slot1.getCost());
            UI.SetTokens(playerInventory.GetTokens());
            playerInventory.AddPowerUp(shopOptions[0].GetComponent<PowerUp>());
        }
    }

    public void purchaseItem2()
    {
        PowerUp slot2 = shopOptions[1].GetComponent<PowerUp>();
        if (playerInventory.GetTokens() >= slot2.getCost())
        {
            playerInventory.SubtractTokens(slot2.getCost());
            UI.SetTokens(playerInventory.GetTokens());
            playerInventory.AddPowerUp(shopOptions[1].GetComponent<PowerUp>());
        }
    }

    public void purchaseItem3()
    {
        PowerUp slot3 = shopOptions[2].GetComponent<PowerUp>();
        if (playerInventory.GetTokens() >= slot3.getCost())
        {
            playerInventory.SubtractTokens(slot3.getCost());
            UI.SetTokens(playerInventory.GetTokens());
            playerInventory.AddPowerUp(shopOptions[2].GetComponent<PowerUp>());
        }
    }
}
