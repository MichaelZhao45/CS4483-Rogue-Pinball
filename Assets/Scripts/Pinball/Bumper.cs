using System;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [Header("Bumper Settings")]
    [SerializeField] private float _strength = 0;
    [SerializeField] private int _pointsEarned = 0;

    // Events
    public static event Action<Bumper> OnBumperHit;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            OnBumperHit?.Invoke(this);

            // If the bumper has no knockback strength, don't bother doing the calculations.
            if (_strength == 0) return;
        
            Vector3 knockback = transform.position - collision.gameObject.transform.position;
            knockback *= _strength;

            _collidedBallRb.AddForce(knockback.normalized, ForceMode.Impulse);
        }
    }

    public int GetPoints()
    {
        return _pointsEarned;
    }
}
