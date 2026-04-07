using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Drain : BaseBoardPiece
{
    public static event Action OnDrainHit;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            Destroy(collision.gameObject);

            if (_isActive)
            {
                OnDrainHit?.Invoke();
                _audio.Play();
            }
        }
    }
}
