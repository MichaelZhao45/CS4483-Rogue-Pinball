using UnityEngine;

public class BaseBoardPiece : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] protected AudioSource _audio;

    [Header("Particle Effects")]
    [SerializeField] protected ParticleSystem _hitVFX;

    protected bool _isActive = true;

    protected void OnEnable()
    {
        RoundManager.RoundStart += Activate;
        RoundManager.RoundOver += Deactivate;
        RoundManager.LastRoundOver += Deactivate;
    }
    
    protected void OnDisable()
    {
        RoundManager.RoundStart -= Activate;
        RoundManager.RoundOver -= Deactivate;
        RoundManager.LastRoundOver -= Deactivate;
    }

    protected void Activate(int round)
    {
        _isActive = true;
    }

    protected void Deactivate()
    {
        _isActive = false;
    }
}
