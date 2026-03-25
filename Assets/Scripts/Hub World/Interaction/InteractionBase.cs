using UnityEngine;

public class InteractionBase : MonoBehaviour
{
    protected PlayerController player;
    protected bool playerNearby;
    [SerializeField] protected string interactionMessage;
    
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player.ShowInteractionText(interactionMessage);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            player.ClearInteractionText();
        }
    }
}
