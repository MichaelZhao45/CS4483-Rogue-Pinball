using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Drain : MonoBehaviour
{
    public static event Action OnDrainHit;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            OnDrainHit?.Invoke();
            _audio.Play();
            Destroy(collision.gameObject);
        }
    }
}
