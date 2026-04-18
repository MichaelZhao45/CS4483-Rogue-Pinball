using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    [Header("Music")] 
    [SerializeField] private AudioClip _pinballMusic;
    [SerializeField] private AudioClip _gameOverMusic;
    [SerializeField] private AudioClip _gameWonMusic;
    [SerializeField] private AudioClip _hubWorldMusic;
    [SerializeField] private AudioClip _shopMusic;

    void OnEnable()
    {
        GameController.GameStarted += PlayPinballMusic;
        GameController.GameContinued += PlayPinballMusic;
        GameController.GameOver += PlayGameOverMusic;
        GameController.GameWon += PlayGameWonMusic;

        Shop.ShopOpened += PlayShopMusic;
    }

    void OnDisable()
    {
        GameController.GameStarted -= PlayPinballMusic;
        GameController.GameContinued -= PlayPinballMusic;
        GameController.GameOver -= PlayGameOverMusic;
        GameController.GameWon -= PlayGameWonMusic;

        Shop.ShopOpened -= PlayShopMusic;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameOverMusic()
    {
        _audioSource.clip = _gameOverMusic;
        _audioSource.Play();
    }

    public void PlayGameWonMusic()
    {
        _audioSource.clip = _gameWonMusic;
        _audioSource.Play();
    }

    public void PlayPinballMusic()
    {
        _audioSource.clip = _pinballMusic;
        _audioSource.Play();
    }

    public void PlayShopMusic()
    {
        _audioSource.clip = _shopMusic;
        _audioSource.Play();
    }

    public void PlayHubMusic()
    {
        _audioSource.clip = _hubWorldMusic;
        _audioSource.Play();
    }
}
