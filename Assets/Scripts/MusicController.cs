using System;
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
    }

    void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        GameController.GameOver -= OnGameOver;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnGameOver()
    {
        _audioSource.clip = _gameOverMusic;
        _audioSource.Play();
    }

    private void OnGameStarted()
    {
        _audioSource.clip = _pinballMusic;
        _audioSource.Play();
    }
}
