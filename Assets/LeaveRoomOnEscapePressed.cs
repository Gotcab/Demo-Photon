using UnityEngine;

public class LeaveRoomOnEscapePressed : MonoBehaviour
{
    public OnlineGameManager gameManager;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameManager.LeaveRoom();
        } 
    }
}
