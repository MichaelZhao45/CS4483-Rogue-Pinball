using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public BallManager ballManager;
    public ScoreManager scoreManager;
    public UIManager UI;

    public void UsePowerUp(PowerUp powerUp)
    {
        switch (powerUp.type)
        {
            case PowerUpType.Clone:
                Debug.Log("Clone powerup used!");

                GameObject[] ballsInPlay = GameObject.FindGameObjectsWithTag("Pinball");
                foreach (GameObject ball in ballsInPlay)
                {
                    ballManager.SpawnBall(ball.transform.position);
                }

                break;
            case PowerUpType.Booster:
                Debug.Log("Booster powerup used!");

                scoreManager.AddMultiplier(1);
                UI.ShowMultiplier(true);

                break;
            default:
                Debug.Log("ERROR: used powerup has unknown enum?");
                break;
        }
    }
}
