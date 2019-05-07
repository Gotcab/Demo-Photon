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

    public SpawnPoint[] spawns;

    private int spawnIndex;

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

    public void LeaveRoom()
    {
        photonView.RPC(nameof(ReleaseSpawnPoint), RpcTarget.Others, new object[] { spawnIndex, photonView.OwnerActorNr });
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        SpawnPoint spawn = GetAvailableSpawn();
        Color playerColor = spawn.playerColor;
        PhotonNetwork.Instantiate(playerPrefab.name, spawn.transform.position, spawn.transform.rotation, 0, new object[] { playerColor.r, playerColor.g, playerColor.b });

    }

    private SpawnPoint GetAvailableSpawn()
    {
        SpawnPoint spawn = null;
        for (int i = 0; i < spawns.Length; i++)
        {
            if (spawns[i].IsAvailable)
            {
                spawn = spawns[i];
                spawnIndex = i;
                photonView.RPC(nameof(UseSpawnPoint), RpcTarget.AllBufferedViaServer, new object[] { spawnIndex, PhotonNetwork.LocalPlayer.ActorNumber });
                break;
            }
        }
        return spawn;
    }

    [PunRPC]
    public void UseSpawnPoint(int index, int playerId)
    {
        spawns[index].Use(playerId);
    }

    [PunRPC]
    public void ReleaseSpawnPoint(int index, int playerId)
    {
        spawns[index].Release(playerId);
    }
}
