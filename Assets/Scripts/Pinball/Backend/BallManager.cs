using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject _currentBallPrefab;

    private int _activeBalls = 0;

    public static event Action AllBallsDrained; 

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStarted;
        Drain.OnDrainHit += OnBallDrained;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        Drain.OnDrainHit -= OnBallDrained;
    }

    private void OnGameStarted()
    {
        _activeBalls = 0;
    }

    private void OnBallDrained()
    {
        _activeBalls--;
        if (_activeBalls == 0) AllBallsDrained?.Invoke();
    }

    public void SpawnBall(Vector3 _spawnPosition)
    {
        Instantiate(_currentBallPrefab, _spawnPosition, Quaternion.identity);
        _activeBalls++;
    }

    public void SetCurrentBallPrefab(GameObject ballPrefab)
    {
        _currentBallPrefab = ballPrefab;
    }

    public GameObject GetCurrentBallPrefab()
    {
        return _currentBallPrefab;
    }

    public int GetActiveBalls()
    {
        return _activeBalls;
    }
}
