using UnityEngine;

public class CloneScript : PowerUp
{
    public override void OnUse()
    {
        //inventoryManager should call this function upon being consumed
        //if pinball map is being used, and game is currently being played:
        //create a second pinball at the current pinballs location, and send both balls in opposite horizontal directions
        //then delete the powerup object
    }
}
