using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.InputSystem; 

public class Inventory : MonoBehaviour
{
    [SerializeField] private RoundManager _roundManager;
    
    // UI Fields
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Color _highlightColor = Color.yellow;
    private Color _aquaBlue;

    // Settings
    [SerializeField] private int _maxInventorySize = 9;

    private PowerUp[] _powerupComponents; 
    private Image[] _uiIcons;
    private Image[] _slotBackgrounds;
    private int _currentlySelectedIndex = -1;

    void Awake()
    {
        ColorUtility.TryParseHtmlString("#04CFE9", out _aquaBlue);

        _powerupComponents = GetComponentsInChildren<PowerUp>(true);
        
        _uiIcons = new Image[_powerupComponents.Length];
        _slotBackgrounds = new Image[_powerupComponents.Length];

        for (int i = 0; i < _powerupComponents.Length; i++)
        {
            _uiIcons[i] = _powerupComponents[i].GetComponent<Image>();
            _slotBackgrounds[i] = _powerupComponents[i].transform.parent.GetComponent<Image>();

            if (_slotBackgrounds[i] != null) _slotBackgrounds[i].color = _aquaBlue;
            
            if (_uiIcons[i] != null)
            {
                _uiIcons[i].sprite = null;
                _uiIcons[i].color = new Color(0, 0, 0, 0); 
            }
        }
    }

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
        if (index < 0 || index >= _powerupComponents.Length || index >= _maxInventorySize) return;

        if (_currentlySelectedIndex != -1 && _currentlySelectedIndex < _slotBackgrounds.Length)
        {
            if (_slotBackgrounds[_currentlySelectedIndex] != null)
                _slotBackgrounds[_currentlySelectedIndex].color = _aquaBlue;
        }

        _currentlySelectedIndex = index;
        if (_slotBackgrounds[_currentlySelectedIndex] != null)
            _slotBackgrounds[_currentlySelectedIndex].color = _highlightColor;

        if (_descriptionText != null)
        {
            string desc = _powerupComponents[index].getDescription();
            _descriptionText.text = string.IsNullOrEmpty(desc) ? "Empty Slot" : desc;
        }
    }

    public void AddPowerUp(PowerUp item)
    {
        for (int i = 0; i < _powerupComponents.Length; i++)
        {
            if (string.IsNullOrEmpty(_powerupComponents[i].getName()))
            {
                _powerupComponents[i].Initialize(
                    item.getName(), 
                    item.getDescription(), 
                    item.getImage(), 
                    item.isConsumable()
                );

                UpdateSlotIcon(i, _powerupComponents[i].getImage()); 
                
                return;
            }
        }
        Debug.LogWarning("Inventory is full!");
    }

    private void UpdateSlotIcon(int index, Sprite img)
    {
        if (index < 0 || index >= _uiIcons.Length || _uiIcons[index] == null) return;

        if (img != null)
        {
            _uiIcons[index].sprite = img;
            _uiIcons[index].color = Color.white; 
            _uiIcons[index].enabled = true;
        }
        else
        {
            _uiIcons[index].sprite = null;
            _uiIcons[index].color = new Color(0, 0, 0, 0);
        }
    }

    private void UseSelectedItem()
    {
        if (_currentlySelectedIndex != -1 && _currentlySelectedIndex < _powerupComponents.Length)
        {
            PowerUp selectedPowerUp = _powerupComponents[_currentlySelectedIndex];
            if (selectedPowerUp != null && !string.IsNullOrEmpty(selectedPowerUp.getName()))
            {
                selectedPowerUp.OnUse();
            }
        }
    }

    public int GetInventorySize() 
    { 
        return _maxInventorySize; 
    }

    private int _tokens = 300;
    public void AddTokens(int amount) { _tokens += amount; }
    public void SubtractTokens(int amount) { _tokens -= amount; if (_tokens < 0) _tokens = 0; }
    public int GetTokens() { return _tokens; }

    void OnEnable() { GameController.GameStarted += ResetInv; GameController.GameOver += ResetInv; }
    void OnDisable() { GameController.GameStarted -= ResetInv; GameController.GameOver -= ResetInv; }
    void ResetInv() { _tokens = 0; }
}