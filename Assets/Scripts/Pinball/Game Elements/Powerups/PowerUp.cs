using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Texture2D _image;
    [SerializeField] private int _cost;

    public string getName()
    {
        return _name;
    }

    public string getDescription()
    {
        return _description;
    }

    public Texture2D getImage()
    {
        return _image;
    }

    public int getCost()
    {
        return _cost;
    }

}
