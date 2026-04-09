using UnityEngine;

public class ScoreBooster : PowerUp
{
    [SerializeField] private ScoreManager scoreManager;

    public override void OnUse()
    {
        scoreManager.AddMultiplier(1);
    }
}
