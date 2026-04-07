using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameController.GameOver += Unlock;
        RoundManager.RoundOver += Unlock;
        Shop.ShopOpened += Unlock;
    }

    private void OnDisable()
    {
        GameController.GameOver -= Unlock;
        RoundManager.RoundOver -= Unlock;
        Shop.ShopOpened -= Unlock;
    }

    public void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
