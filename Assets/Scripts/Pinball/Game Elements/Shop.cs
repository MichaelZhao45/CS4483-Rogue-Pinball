using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Canvas _shopCanvas;
    
    public void SetVisible(bool state)
    {
        _shopCanvas.gameObject.SetActive(state);
    }
}
