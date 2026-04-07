using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.InputSystem; // Required for the 1-5 key detection

public class Inventory : MonoBehaviour
{
    [SerializeField] private RoundManager _roundManager;
    
    // --- New UI Fields ---
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Color _highlightColor = Color.yellow;
    [SerializeField] private Color _normalColor = Color.white;

    private int _money = 0;
    [SerializeField] private int _maxInventorySize = 5;

    private PowerUp[] _powerupComponents;
    private Image[] _uiSlots;
    private int _currentlySelectedIndex = -1;

    // --- New Logic to Initialize and Detect Keys ---

    void Start()
    {
        // Finds the PowerUp scripts and Images on your grid children
        _powerupComponents = GetComponentsInChildren<PowerUp>();
        _uiSlots = new Image[_powerupComponents.Length];

        for (int i = 0; i < _powerupComponents.Length; i++)
        {
            _uiSlots[i] = _powerupComponents[i].GetComponent<Image>();
        }
    }

    void Update()
    {
        // Check for Keys 1-5 using New Input System syntax
        if (Keyboard.current == null) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectSlot(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectSlot(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectSlot(2);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) SelectSlot(3);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) SelectSlot(4);
    }

    private void SelectSlot(int index)
    {
        // Ensure we don't go out of bounds of our actual children or max size
        if (index >= _powerupComponents.Length || index >= _maxInventorySize) return;

        // Reset previous highlight
        if (_currentlySelectedIndex != -1)
            _uiSlots[_currentlySelectedIndex].color = _normalColor;

        // Apply new highlight
        _currentlySelectedIndex = index;
        _uiSlots[_currentlySelectedIndex].color = _highlightColor;

        // Update the description text
        if (_descriptionText != null)
            _descriptionText.text = _powerupComponents[index].getDescription();
    }

    // --- Your original methods preserved below ---

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
        _money = 0;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
    }

    public void SubtractMoney(int amount)
    {
        _money -= amount;
        if (_money < 0) _money = 0;
    }

    public int GetMoney()
    {
        return _money;
    }
}