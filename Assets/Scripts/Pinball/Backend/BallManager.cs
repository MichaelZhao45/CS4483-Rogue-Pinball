using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject _currentBallPrefab;

    private int _activeBalls;

    private void OnEnable()
    {
        GameController.GameStarted += OnGameStarted;
        Drain.OnDrainHit += OnBallDrained;
        //RoundManager.RoundOver += DestroyAllBalls;
    }

    private void OnDisable()
    {
        GameController.GameStarted -= OnGameStarted;
        Drain.OnDrainHit -= OnBallDrained;
        //RoundManager.RoundOver -= DestroyAllBalls;
    }

    private void OnGameStarted()
    {
        _activeBalls = 0;
    }

    private void OnBallDrained()
    {
        _activeBalls--;
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

    public void DestroyAllBalls()
    {
        GameObject foundBall;
        while ((foundBall = GameObject.FindWithTag("Pinball")) != null)
        {
            Destroy(foundBall);
        }

        Debug.Log("All balls destroyed.");
    }
}
