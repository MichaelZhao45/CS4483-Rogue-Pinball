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
    [SerializeField] private Color _normalColor = Color.white;

    // Currency (Merged to use Tokens)
    private int _tokens = 0;
    [SerializeField] private int _maxInventorySize = 5;

    private PowerUp[] _powerupComponents;
    private Image[] _uiSlots;
    private int _currentlySelectedIndex = -1;

    void Start()
    {
        _powerupComponents = GetComponentsInChildren<PowerUp>();
        _uiSlots = new Image[_powerupComponents.Length];

        for (int i = 0; i < _powerupComponents.Length; i++)
        {
            _uiSlots[i] = _powerupComponents[i].GetComponent<Image>();
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
        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            UseSelectedItem();
        }
    }

    private void UseSelectedItem()
    {
        if (_currentlySelectedIndex != -1 && _currentlySelectedIndex < _powerupComponents.Length)
        {
            PowerUp selectedPowerUp = _powerupComponents[_currentlySelectedIndex];

            if (selectedPowerUp != null)
            {
                Debug.Log("Using power-up: " + selectedPowerUp.getName());
                selectedPowerUp.OnUse();

                if (selectedPowerUp.isConsumable())
                {
                    // Logic to remove item or clear slot could go here
                }
            }
        }
    }

    private void SelectSlot(int index)
    {
        _powerupComponents = GetComponentsInChildren<PowerUp>();
        
        if (index >= _powerupComponents.Length || index >= _maxInventorySize) return;

        // 1. Handle Highlight (The background "Cell")
        if (_currentlySelectedIndex != -1)
            _uiSlots[_currentlySelectedIndex].color = _normalColor;

        _currentlySelectedIndex = index;
        _uiSlots[_currentlySelectedIndex].color = _highlightColor;

        // 2. Handle the Icon Overlap
        Texture2D tex = _powerupComponents[index].getImage();
        
        // Find the 'Icon' child of the current slot
        Image iconImage = _uiSlots[index].transform.Find("Icon").GetComponent<Image>();

        if (tex != null && iconImage != null)
        {
            // Convert Texture2D to Sprite
            Rect rect = new Rect(0, 0, tex.width, tex.height);
            iconImage.sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f));
            iconImage.enabled = true; // Show icon
        }
        else if (iconImage != null)
        {
            iconImage.enabled = false; // Hide icon if slot is empty
        }

        // 3. Update Description
        if (_descriptionText != null)
            _descriptionText.text = _powerupComponents[index].getDescription();
    }

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

    public void SubtractTokens(int amount)
    {
        _tokens -= amount;
        if (_tokens < 0) _tokens = 0;
    }

    public int GetTokens()
    {
        return _tokens;
    }
}