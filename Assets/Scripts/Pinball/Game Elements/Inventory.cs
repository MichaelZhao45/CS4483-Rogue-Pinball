using UnityEngine;

public class Inventory : MonoBehaviour
{
    private int _money = 0;
    [SerializeField] private int _maxInventorySize = 5;

    // private Powerup _powerups[];

    void Start()
    {
        _money = 0;
    }
}
