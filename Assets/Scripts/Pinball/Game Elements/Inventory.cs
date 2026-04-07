using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private RoundManager _roundManager;
    
    private int _tokens = 0;
    [SerializeField] private int _maxInventorySize = 5;

    private PowerUp[] _powerups;

    void OnEnable()
    {
        GameController.GameStarted += Reset;
        GameController.GameOver += Reset;
    }

    void OnDisable()
    {
        GameController.GameStarted -= Reset;
        GameController.GameOver -= Reset;
    }

    void Reset()
    {
        _tokens = 0;
    }

    public void AddTokens(int amount)
    {
        _tokens += amount;
    }

    public void SubtractMoney(int amount)
    {
        _tokens -= amount;

        // Ensure that the player's money count does not drop under zero.
        if (_tokens < 0) _tokens = 0;
    }

    public int GetTokens()
    {
        return _tokens;
    }
}
