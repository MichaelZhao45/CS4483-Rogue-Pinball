using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Canvas _shopCanvas;

    public static event Action ShopOpened;
    
    public void Show()
    {
        _shopCanvas.gameObject.SetActive(true);
        ShopOpened?.Invoke();
    }

    public void Hide()
    {
        _shopCanvas.gameObject.SetActive(false);
    }
}
