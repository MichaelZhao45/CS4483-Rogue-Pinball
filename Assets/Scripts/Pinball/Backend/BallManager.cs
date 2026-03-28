using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject _currentBallPrefab;

    public void SpawnBall(Vector3 _spawnPosition)
    {
        Instantiate(_currentBallPrefab, _spawnPosition, Quaternion.identity);
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
