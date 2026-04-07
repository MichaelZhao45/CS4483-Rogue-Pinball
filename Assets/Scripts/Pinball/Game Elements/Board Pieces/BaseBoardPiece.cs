using UnityEngine;

public class BaseBoardPiece : MonoBehaviour
{
    [SerializeField] protected AudioSource _audio;

    protected bool _isActive = true;

    protected void OnEnable()
    {
        RoundManager.RoundStart += Activate;
        RoundManager.RoundOver += Deactivate;
    }
    
    protected void OnDisable()
    {
        RoundManager.RoundStart -= Activate;
        RoundManager.RoundOver -= Deactivate;
    }

    protected void Activate()
    {
        _isActive = true;
    }

    protected void Deactivate()
    {
        _isActive = false;
    }
}
