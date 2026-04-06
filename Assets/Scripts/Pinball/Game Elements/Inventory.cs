using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private RoundManager _roundManager;
    
    private int _money = 0;
    [SerializeField] private int _maxInventorySize = 5;

    private PowerUp[] _powerups;

    void OnEnable()
    {
        GameController.GameStarted += Reset;
        GameController.GameEnded += Reset;
    }

    void OnDisable()
    {
        GameController.GameStarted -= Reset;
        GameController.GameEnded -= Reset;
    }

    void Reset()
    {
        _money = 0;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
    }

    public void SubtractMoney(int amount)
    {
        _money -= amount;

        // Ensure that the player's money count does not drop under zero.
        if (_money < 0) _money = 0;
    }

    public int GetMoney()
    {
        return _money;
    }
}
