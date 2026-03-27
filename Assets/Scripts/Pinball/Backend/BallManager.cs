using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject _currentBallPrefab;
    private GameObject _startingBallInstance;

    public void SpawnBall(Vector3 _spawnPosition)
    {
        _startingBallInstance = Instantiate(_currentBallPrefab, _spawnPosition, Quaternion.identity);
    }

    public GameObject GetStartingBall()
    {
        return _startingBallInstance;
    }

    public void SetCurrentBallPrefab(GameObject ballPrefab)
    {
        _currentBallPrefab = ballPrefab;
    }

    public GameObject GetCurrentBallPrefab()
    {
        return _currentBallPrefab;
    }
}
