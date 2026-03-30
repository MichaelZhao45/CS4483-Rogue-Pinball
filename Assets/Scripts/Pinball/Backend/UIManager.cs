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
    }

    /*
    public void SetValue(ref TMP_Text UIText, int num)
    {
        UIText.text = num.ToString();
    }

    public void SetVisible(ref Canvas canvas, bool state)
    {
        canvas.gameObject.SetActive(state);
    }
    */

    public void SetScore(int score)
    {
        _scoreCounterText.text = score.ToString();
    }

    public void SetThreshold(int threshold)
    {
        _scoreThresholdText.text = threshold.ToString();
    }

    public void SetRound(int round)
    {
        _roundCounterText.text = round.ToString();
    }

    public void SetFinalScore(int score)
    {
        _finalScoreText.text = score.ToString();
    }

    public void SetRoundReached(int round)
    {
        _roundReachedText.text = round.ToString();
    }

    public void SetBalls(int ballsRemaining)
    {
        _ballsRemainingText.text = ballsRemaining.ToString();
    }

    public void ShowGameInterface(bool state)
    {
        _interfaceCanvas.gameObject.SetActive(state);
    }

    public void ShowGameOver(bool state)
    {
        _gameOverCanvas.gameObject.SetActive(state);
    }

    public void ShowHelp(bool state)
    {
        _helpCanvas.gameObject.SetActive(state);
    }

    public void HideAll()
    {
        ShowGameInterface(false);
        ShowGameOver(false);
        ShowHelp(false);
    }
}
