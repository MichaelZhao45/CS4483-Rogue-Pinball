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
    }

    public void Hide()
    {
        _shopCanvas.gameObject.SetActive(false);
        player.TurnOnHUD();
    }
}
