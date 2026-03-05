using UnityEngine;

public class PinballInteraction : MonoBehaviour
{
    public GameObject fpsCamera;
    public GameObject pinballCamera;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            fpsCamera.SetActive(false);
            pinballCamera.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}