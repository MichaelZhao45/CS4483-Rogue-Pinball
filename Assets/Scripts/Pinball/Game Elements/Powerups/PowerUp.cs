using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PowerUp")]
public class PowerUp : ScriptableObject
{
    public PowerUpType type;
    public string description;
    public Sprite image;
    public int cost;
    public bool isConsumable;
}

public enum PowerUpType
{
    Clone,
    Booster
}
