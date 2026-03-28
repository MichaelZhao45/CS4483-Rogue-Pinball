using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ball : MonoBehaviour
{
    private AudioSource _ballAudioSource;

    void Start()
    {
        _ballAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pinball Machine")) _ballAudioSource.Play();
    }
}
