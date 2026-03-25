using System;
using System.Collections;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [Header("Bumper Settings")]
    [SerializeField] private float _strength = 0;
    [SerializeField] private int _pointsEarned = 0;

    private float _bounceChargeTime = 1.0f;
    private bool _isBounceReady = true;

    public static event Action<Bumper> OnBumperHit;

    void OnCollisionEnter(Collision collision)
    {
        if (_isBounceReady && collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            OnBumperHit?.Invoke(this);
            StartCoroutine(HandleBounceCharge());

            // If the bumper has no knockback strength, don't bother doing the calculations.
            if (_strength == 0) return;
        
            Vector3 knockback = collision.contacts[0].normal * _strength;

            _collidedBallRb.AddForce(knockback, ForceMode.Impulse);
        }
    }

    // A timer is necessary to stop weird physics glitches when the ball bounces off a bumper.
    private IEnumerator HandleBounceCharge()
    {
        _isBounceReady = false;
        yield return new WaitForSeconds(_bounceChargeTime);
        _isBounceReady = true;
    }

    public int GetPoints()
    {
        return _pointsEarned;
    }
}
