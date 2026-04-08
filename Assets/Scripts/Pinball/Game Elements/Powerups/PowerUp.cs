using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _image;
    [SerializeField] private int _cost;
    [SerializeField] private bool _consumable;

    public string getName()
    {
        return _name;
    }

    public string getDescription()
    {
        return _description;
    }

    public Sprite getImage()
    {
        return _image;
    }

    public int getCost()
    {
        return _cost;
    }

    public bool isConsumable()
    {
        return _consumable;
    }
    
    //virtual class allows subclass to override function
    public virtual void OnUse()
    {

    }
}
