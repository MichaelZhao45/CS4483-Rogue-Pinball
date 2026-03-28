using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bumper : MonoBehaviour
{
    [Header("Bumper Settings")]
    [SerializeField] private float _strength = 0;
    [SerializeField] private int _pointsEarned = 0;

    [SerializeField] private AudioSource _audio;

    public static event Action<int> OnBumperHit;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            _audio.Play();
            OnBumperHit?.Invoke(_pointsEarned);

            // If the bumper has no knockback strength, don't bother doing the calculations.
            if (_strength == 0) return;
        
            Vector3 knockback = collision.contacts[0].normal * _strength;

            _collidedBallRb.AddForce(knockback, ForceMode.Impulse);
        }
    }

    public int GetPoints()
    {
        return _pointsEarned;
    }
}
