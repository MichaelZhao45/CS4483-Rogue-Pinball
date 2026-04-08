using UnityEngine;

public class CloneScript : PowerUp
{
    [SerializeField] public GameController gameController;
    [SerializeField] public GameObject ball;
    [SerializeField] public BallManager ballManager;

    public override void OnUse()
    {
        Vector3 position = ball.transform.position;
        ballManager.SpawnBall(position);
        Destroy(gameObject);
    }
}
