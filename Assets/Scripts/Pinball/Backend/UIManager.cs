using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public RoundManager roundManager;

    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Canvas _interfaceCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Canvas _helpCanvas;
    [SerializeField] private TMP_Text _roundCounterText;
    [SerializeField] private TMP_Text _scoreCounterText;
    [SerializeField] private TMP_Text _scoreThresholdText;
    [SerializeField] private TMP_Text _ballsRemainingText;
    [SerializeField] private TMP_Text _finalScoreText;
    [SerializeField] private TMP_Text _roundReachedText;
    [SerializeField] private TMP_Text _moneyCounterText;

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStarted;
        GameController.GameOver += OnGameOver;
        RoundManager.RoundChanged += SetRound;
        BallDropper.BallDropped += OnBallDropped;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        GameController.GameOver -= OnGameOver;
        RoundManager.RoundChanged -= SetRound;
        BallDropper.BallDropped -= OnBallDropped;
    }

    private void OnGameStarted()
    {
        InitializeText();
        ShowGameInterface(true);
        ShowGameOver(false);
        ShowHelp(true);
    }

    private void OnGameOver()
    {
        ShowGameInterface(false);

        SetFinalScore(scoreManager.GetCurrentScore());
        SetRoundReached(roundManager.GetCurrentRound());

        ShowGameOver(true);
    }

    private void OnBallDropped()
    {
        ShowHelp(false);
    }

    private void InitializeText()
    {
        SetThreshold(250);
        SetRound(1);
        SetMoney(0);
    }
    private void SetValue(TMP_Text textElement, int num)
    {
        textElement.text = num.ToString();
    }

    private void SetVisible(Canvas canvas, bool state)
    {
        canvas.gameObject.SetActive(state);
    }

    public void SetScore(int score)
    {
        SetValue(_scoreCounterText, score);
    }

    public void SetThreshold(int threshold)
    {
        SetValue(_scoreThresholdText, threshold);
    }

    public void SetRound(int round)
    {
        SetValue(_roundCounterText, round);
    }

    public void SetFinalScore(int score)
    {
        SetValue(_finalScoreText, score);
    }

    public void SetRoundReached(int round)
    {
        SetValue(_roundReachedText, round);
    }

    public void SetBalls(int ballsRemaining)
    {
        SetValue(_ballsRemainingText, ballsRemaining);
    }

    public void SetMoney(int amount)
    {
        _moneyCounterText.text = $"${amount}";
    }

    public void ShowGameInterface(bool state)
    {
        SetVisible(_interfaceCanvas, state);
    }

    public void ShowGameOver(bool state)
    {
        SetVisible(_gameOverCanvas, state);
    }

    public void ShowHelp(bool state)
    {
        SetVisible(_helpCanvas, state);
    }

    public void HideAll()
    {
        ShowGameInterface(false);
        ShowGameOver(false);
        ShowHelp(false);
    }
}
