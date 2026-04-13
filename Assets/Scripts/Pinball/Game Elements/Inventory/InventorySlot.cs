using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventorySlot : MonoBehaviour
{
    private Image _background;
    public Color selectedColour = Color.yellow, notSelectedColour = Color.aquamarine;

    private void Awake()
    {
        _background = GetComponent<Image>();

        Deselect();
    }

    public void Select()
    {
        _background.color = selectedColour;
    }

    public void Deselect()
    {
        _background.color = notSelectedColour;
    }
}
