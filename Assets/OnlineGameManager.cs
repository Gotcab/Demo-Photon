using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI text;
    public GameObject playerPrefab;

    public Color[] playerColors = new Color[4];

    public Transform[] spawns = new Transform[4];

    public bool[] availableSlots = new bool[4] { true, true, true, true };

    // Not seen by you
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        text.text = text.text + Environment.NewLine + $"Player {newPlayer.ActorNumber} joined the room";       
    }

    //No seen by you
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        text.text = text.text + Environment.NewLine + $"Player {otherPlayer.ActorNumber} left the room";
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        // Ask to instantiate our player
        photonView.RPC(nameof(RequestPlayer), RpcTarget.MasterClient, new object[] { photonView.OwnerActorNr });
            
    }

    /// <summary>
    /// This is executed on the MasterClient
    /// </summary>
    /// <param name="actor"></param>
    [PunRPC]
    private void RequestPlayer(int actor)
    {
        int slot = GetAvailableSlot();
        photonView.RPC(
            nameof(SpawnPlayer), 
            PhotonNetwork.PlayerList[actor], 
            new object[] { spawns[slot].position, spawns[slot].rotation });
    }

    /// <summary>
    /// This is executed on the client requesting a player
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    [PunRPC]
    private void SpawnPlayer(Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate(playerPrefab.name, position, rotation);
    }

    private int GetAvailableSlot()
    {
        int i;

        for (i = 0; i < availableSlots.Length; i++)
        {
            if (availableSlots[i] == true)
            {          
                break;
            }
        }
        return i;
    }
}
