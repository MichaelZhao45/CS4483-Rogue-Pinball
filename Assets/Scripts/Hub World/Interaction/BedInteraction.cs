using UnityEngine;

public class BedInteraction : InteractionBase
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) pc.ShowBedInteractionText();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) pc.ClearInteractionText();
    }
}
