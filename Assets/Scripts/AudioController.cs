using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;

    [Header("Music")] 
    [SerializeField] private AudioClip _pinballMusic;
    [SerializeField] private AudioClip _gameOverMusic;
    [SerializeField] private AudioClip _hubWorldMusic;
    [SerializeField] private AudioClip _shopMusic;

    //[Header("SFX")]
    //[Serialize] private AudioClip _ballHit;

    void OnEnable()
    {
        GameController.GameStarted += OnGameStarted;
        GameController.GameEnded += OnGameEnded;
        Shop.ShopOpened += OnShopOpened;
    }

    void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        GameController.GameEnded -= OnGameEnded;
        Shop.ShopOpened -= OnShopOpened;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnGameEnded()
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
