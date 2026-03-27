using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Canvas _interfaceCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private TMP_Text _roundCounterText;
    [SerializeField] private TMP_Text _scoreCounterText;
    [SerializeField] private TMP_Text _scoreThresholdText;
    [SerializeField] private TMP_Text _ballsRemainingText;

    void Start()
    {
        _scoreThresholdText.text = "0";
        _roundCounterText.text = "1";
    }

    public void SetScore(int score)
    {
        _scoreCounterText.text = score.ToString();
    }

    public void SetBalls(int ballsRemaining)
    {
        _ballsRemainingText.text = ballsRemaining.ToString();
    }

    public void ShowGameInterface(bool state)
    {
        Debug.Log($"Game Interface Enabled: {state}");
        _interfaceCanvas.gameObject.SetActive(state);
    }

    public void ShowGameOver(bool state)
    {
        Debug.Log($"GameOver Screen Enabled: {state}");
        _gameOverCanvas.gameObject.SetActive(state);
    }
}
