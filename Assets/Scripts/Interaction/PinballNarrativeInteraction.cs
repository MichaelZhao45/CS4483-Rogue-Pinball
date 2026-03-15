using UnityEngine;

public class PinballNarrativeInteraction : InteractionBase
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) pc.ShowPinballNarrativeInteractionText();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) pc.ClearInteractionText();
    }
}
