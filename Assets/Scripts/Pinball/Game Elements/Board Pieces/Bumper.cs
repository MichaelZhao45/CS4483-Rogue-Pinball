using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bumper : BaseBoardPiece
{
    [Header("Bumper Settings")]
    [SerializeField] private float _strength = 0;
    [SerializeField] private int _pointsEarned = 0;

    public static event Action<int> OnBumperHit;

    void OnCollisionEnter(Collision collision)
    {
        if (!_isActive) return;

        if (collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            if (_audio != null) _audio.Play();
            if (_hitVFX != null) _hitVFX.Play();
            
            OnBumperHit?.Invoke(_pointsEarned);

            // If the bumper has no knockback strength, don't bother doing the calculations.
            if (_strength <= 0) return;
        
            Vector3 knockback = collision.contacts[0].normal * _strength;

            _collidedBallRb.AddForce(knockback, ForceMode.Impulse);
        }
    }
}
