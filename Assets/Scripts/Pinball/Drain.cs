using System;
using UnityEngine;

public class Drain : MonoBehaviour
{
    public static event Action<Drain> OnDrainHit;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out var _collidedBallRb))
        {
            OnDrainHit?.Invoke(this);
            Destroy(collision.gameObject);
        }
    }
}
