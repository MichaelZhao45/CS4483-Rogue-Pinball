using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Canvas _shopCanvas;
    [SerializeField] private PlayerController player;

    public static event Action ShopOpened;
    
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
