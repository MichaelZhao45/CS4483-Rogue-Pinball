using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _pinballMusic;
    [SerializeField] private AudioClip _gameOverMusic;
    [SerializeField] private AudioClip _hubWorldMusic;
    [SerializeField] private AudioClip _shopMusic;

    void OnEnable()
    {
        GameController.GameStarted += OnGameStarted;
        GameController.GameOver += OnGameOver;
        Shop.ShopOpened += OnShopOpened;
    }

    void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        GameController.GameOver -= OnGameOver;
        Shop.ShopOpened -= OnShopOpened;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnGameOver()
    {
        _audioSource.clip = _gameOverMusic;
        _audioSource.Play();
    }

    public void OnGameStarted()
    {
        _audioSource.clip = _pinballMusic;
        _audioSource.Play();
    }

    public void OnShopOpened()
    {
        _audioSource.clip = _shopMusic;
        _audioSource.Play();
    }

    public void OnReturnToHub()
    {
        _audioSource.clip = _hubWorldMusic;
        _audioSource.Play();
    }
}
