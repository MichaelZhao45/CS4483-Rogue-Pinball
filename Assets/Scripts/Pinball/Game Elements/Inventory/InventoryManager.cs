using UnityEngine;
using TMPro; 
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private PowerUpController _powerUpController;
    
    [Header("UI Fields")]
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [Header("Inventory Settings")]
    [SerializeField] private int _inventorySize = 9;

    [SerializeField] private InventorySlot[] _inventorySlots;
    public GameObject inventoryPowerUpPrefab;

    private int _selectedSlot = -1;
    private InventoryPowerUp _selectedInvPowerUp = null;

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectSlot(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectSlot(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectSlot(2);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) SelectSlot(3);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) SelectSlot(4);
        if (Keyboard.current.digit6Key.wasPressedThisFrame) SelectSlot(5);
        if (Keyboard.current.digit7Key.wasPressedThisFrame) SelectSlot(6);
        if (Keyboard.current.digit8Key.wasPressedThisFrame) SelectSlot(7);
        if (Keyboard.current.digit9Key.wasPressedThisFrame) SelectSlot(8);
        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            UseSelectedItem();
        }
    }

    private void SelectSlot(int index)
    {
        // Disable previous selection.
        if (_selectedSlot >= 0) _inventorySlots[_selectedSlot].Deselect();

        // Highlight new selection.
        _selectedSlot = index;
        _inventorySlots[_selectedSlot].Select();

        // Show the description text for the new selection.
        _selectedInvPowerUp = _inventorySlots[_selectedSlot].GetComponentInChildren<InventoryPowerUp>();
        if (_selectedInvPowerUp != null) _descriptionText.text = _selectedInvPowerUp.GetPowerUp().description;
        else _descriptionText.text = "Empty Slot";
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        // Iterate through all slots to find a free one.
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            InventorySlot slot = _inventorySlots[i];
            InventoryPowerUp powerUpInSlot = slot.GetComponentInChildren<InventoryPowerUp>();

            if (powerUpInSlot == null)
            {
                SpawnPowerUp(powerUp, slot);
                return;
            }
        }

        Debug.LogWarning("Inventory is full!");
    }

    private void SpawnPowerUp(PowerUp powerUp, InventorySlot slot)
    {
        GameObject newPowerUp = Instantiate(inventoryPowerUpPrefab, slot.transform);
        InventoryPowerUp inventoryPowerUp = newPowerUp.GetComponent<InventoryPowerUp>();
        inventoryPowerUp.InitializeItem(powerUp);
    }

    private void UseSelectedItem()
    {
        if (_selectedInvPowerUp != null)
        {
            PowerUp powerUp = _selectedInvPowerUp.GetPowerUp();

            _descriptionText.text = $"{powerUp.type} PowerUp Activated!";

            _powerUpController.UsePowerUp(powerUp);

            Destroy(_selectedInvPowerUp.gameObject);
        }
        else
        {
            _descriptionText.text = "No PowerUp To Use!";
        }
    }

    public int GetInventorySize() 
    { 
        return _inventorySize; 
    }

    private int _tokens = 300;
    public void AddTokens(int amount) { _tokens += amount; }
    public void SubtractTokens(int amount) { _tokens -= amount; if (_tokens < 0) _tokens = 0; }
    public int GetTokens() { return _tokens; }

    void OnEnable()
    {
        GameController.GameStarted += ResetInv;
        GameController.GameOver += ResetInv;
    }

    void OnDisable()
    {
        GameController.GameStarted -= ResetInv;
        GameController.GameOver -= ResetInv;
    }

    void ResetInv()
    {
        _tokens = 0;
    }
}