using Photon.Pun;
using UnityEngine;

public class LeaveRoomOnEscapePressed : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        } 
    }
}
