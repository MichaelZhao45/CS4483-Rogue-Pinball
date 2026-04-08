using UnityEngine;
using TMPro;

public class TokenManager : MonoBehaviour
{
    public Inventory playerInventory;

    public TMP_Text tokensEarned;

    public void DispenseTokens()
    {
        playerInventory.AddTokens(int.Parse(tokensEarned.GetParsedText()));
    }
}
