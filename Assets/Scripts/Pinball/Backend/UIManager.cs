using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Backend Controllers/Managers")]
    public ScoreManager scoreManager;
    public RoundManager roundManager;

    [Header("Canvases")]
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Canvas _interfaceCanvas;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Canvas _helpCanvas;
    [SerializeField] private Canvas _roundOverCanvas;

    [Header("Game Interface")]
    [SerializeField] private TMP_Text _roundCounter;
    [SerializeField] private TMP_Text _scoreCounter;
    [SerializeField] private TMP_Text _scoreThreshold;
    [SerializeField] private TMP_Text _ballsRemaining;
    [SerializeField] private TMP_Text _tokenCounter_Game;
    
    [Header("Game Over")]
    [SerializeField] private TMP_Text _finalScore;
    [SerializeField] private TMP_Text _roundReached;
    
    [Header("Shop")]
    [SerializeField] private TMP_Text _inventoryCapacity;
    [SerializeField] private TMP_Text _tokenCounter_Shop;
    [SerializeField] private TMP_Text _optionName1;
    [SerializeField] private TMP_Text _optionName2;
    [SerializeField] private TMP_Text _optionName3;
    [SerializeField] private TMP_Text _optionPrice1;
    [SerializeField] private TMP_Text _optionPrice2;
    [SerializeField] private TMP_Text _optionPrice3;
    [SerializeField] private Image _optionImage1;
    [SerializeField] private Image _optionImage2;
    [SerializeField] private Image _optionImage3;

    [Header("Round Finished")]
    [SerializeField] private TMP_Text _tokensEarned;
    [SerializeField] private TMP_Text _ballsRemainingBonus;
    [SerializeField] private TMP_Text _roundCompleteReward;

    /* Event Subscriptions */

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStarted;
        GameController.GameOver += OnGameOver;

        RoundManager.RoundStart += OnRoundStart;
        RoundManager.RoundOver += OnRoundOver;

        BallDropper.BallDropped += OnBallDropped;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        GameController.GameOver -= OnGameOver;

        RoundManager.RoundStart -= OnRoundStart;
        RoundManager.RoundOver -= OnRoundOver;

        BallDropper.BallDropped -= OnBallDropped;
    }

    /* Event Reactions */

    // At the beginning of the first round of a *newly-started* run.
    private void OnGameStarted()
    {
        InitializeText();
        ShowGameInterface(true);
        ShowHelp(true);
    }

    // Upon a game over.
    private void OnGameOver()
    {
        ShowGameInterface(false);

        SetFinalScore(scoreManager.GetScore());
        SetRoundReached(roundManager.GetCurrentRound());

        ShowGameOver(true);
    }

    // At the beginning of any round of an *on-going* run.
    private void OnRoundStart()
    {
        InitializeText();
        ShowGameInterface(true);
    }

    private void OnRoundOver()
    {
        ShowGameInterface(false);
        ShowRoundOver(true);
    }

    private void OnBallDropped()
    {
        ShowHelp(false);
    }

    /* Script-Specific Methods */

    private void InitializeText()
    {
        Debug.Log($"UIManager | InitializeText: Setting up UI for round {roundManager.GetCurrentRound()}.");

        SetThreshold(scoreManager.GetScoreThreshold());
        SetRound(roundManager.GetCurrentRound());
        // TODO: set it to the player's current token count
        SetTokens(0);
    }

    /* Getters and Setters */

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
        SetValue(_scoreCounter, score);
    }

    public void SetThreshold(int threshold)
    {
        SetValue(_scoreThreshold, threshold);
    }

    public void SetRound(int round)
    {
        SetValue(_roundCounter, round);
    }

    public void SetFinalScore(int score)
    {
        SetValue(_finalScore, score);
    }

    public void SetRoundReached(int round)
    {
        SetValue(_roundReached, round);
    }

    public void SetBalls(int ballsRemaining)
    {
        SetValue(_ballsRemaining, ballsRemaining);
    }

    public void SetTokens(int amount)
    {
        SetValue(_tokenCounter_Game, amount);
        SetValue(_tokenCounter_Shop, amount);
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

    public void ShowRoundOver(bool state)
    {
        SetVisible(_roundOverCanvas, state);
    }
}
