using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Texture2D image;
    [SerializeField] private int cost;

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        return description;
    }

    public Texture2D getImage()
    {
        return image;
    }

    public int getCost()
    {
        return cost;
    }

}
