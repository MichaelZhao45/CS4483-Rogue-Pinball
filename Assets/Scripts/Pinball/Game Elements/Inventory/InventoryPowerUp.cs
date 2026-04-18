using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryPowerUp : MonoBehaviour
{
    // Holds the information about which powerup is currently being held in the slot.
    private PowerUp _heldPowerUp;

    private Image _image;

    public void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void InitializeItem(PowerUp newPowerUp)
    {
        _heldPowerUp = newPowerUp;
        _image.sprite = _heldPowerUp.image;
    }

    public PowerUp GetPowerUp()
    {
        return _heldPowerUp;
    }
}
