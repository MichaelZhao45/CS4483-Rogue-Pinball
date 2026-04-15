using UnityEngine;
using TMPro; 
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] private PlayerInput _playerInput;

    [Header("Backend")]
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private PowerUpController _powerUpController;
    
    [Header("UI Fields")]
    [SerializeField] private GameObject _descriptionField;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [Header("Inventory Settings")]
    [SerializeField] private int _inventorySize = 9;

    [SerializeField] private InventorySlot[] _inventorySlots;
    public GameObject inventoryPowerUpPrefab;

    private int _selectedSlot = 1;
    private InventoryPowerUp _selectedInvPowerUp = null;

    /* Controlling the Inventory Action Map */

    void Awake()
    {
        DisableInventory();
    }

    public void EnableInventory(int round = 0)
    {
        _descriptionField.SetActive(true);
        _playerInput.actions.FindActionMap("Inventory").Enable();
    }

    public void DisableInventory()
    {
        _descriptionField.SetActive(false);
        _playerInput.actions.FindActionMap("Inventory").Disable();
        _inventorySlots[_selectedSlot].Deselect();
    }

    /* Button Bindings */

    public void Slot1Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(0);
    }

    public void Slot2Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(1);
    }

    public void Slot3Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(2);
    }

    public void Slot4Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(3);
    }

    public void Slot5Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(4);
    }

    public void Slot6Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(5);
    }

    public void Slot7Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(6);
    }

    public void Slot8Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(7);
    }

    public void Slot9Pressed(InputAction.CallbackContext context)
    {
        if (context.performed) SelectSlot(8);
    }

    public void SelectSlot(int index)
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

    public void UseSelectedItem(InputAction.CallbackContext context)
    {
        if (context.performed)
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

        RoundManager.RoundStart += EnableInventory;
        RoundManager.RoundOver += DisableInventory;
    }

    void OnDisable()
    {
        GameController.GameStarted -= ResetInv;
        GameController.GameOver -= ResetInv;

        RoundManager.RoundStart -= EnableInventory;
        RoundManager.RoundOver -= DisableInventory;
    }

    void ResetInv()
    {
        _tokens = 0;
    }
}