using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    protected PlayerController pc;
    protected bool playerNearby;
    
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            //pc.ShowInteractionText();
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            pc.ClearInteractionText();
        }
    }
}
