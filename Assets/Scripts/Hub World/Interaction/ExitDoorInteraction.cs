using UnityEngine;

public class ExitDoorInteraction : InteractionBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game quit");
            Application.Quit();
        }
    }
}
