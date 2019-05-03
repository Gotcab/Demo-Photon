using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public string room;
    public TextMeshProUGUI connectionText;

    private bool isConnecting = false;

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinOrCreateRoom(room, new RoomOptions() { MaxPlayers = 4 }, TypedLobby.Default);
            connectionText.enabled = true;
        }

        else if (!isConnecting)
        {
            PhotonNetwork.ConnectUsingSettings();
            isConnecting = true;
            connectionText.enabled = true;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
        if (isConnecting)
        {
            PhotonNetwork.JoinOrCreateRoom(room, new RoomOptions() { MaxPlayers = 4 }, TypedLobby.Default);
            //PhotonNetwork.JoinRandomRoom();
        }  
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Connection failed: {cause.ToString()}");
        isConnecting = false;
        connectionText.enabled = false;
    }

    public override void OnConnected()
    {
        Debug.Log("Connected to Photon");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        //Go online
        SceneManager.LoadScene(1);
    }

}
