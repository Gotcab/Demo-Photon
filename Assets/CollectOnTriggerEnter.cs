using Photon.Pun;
using UnityEngine;
using TMPro;
using System;

public class CollectOnTriggerEnter : MonoBehaviourPun
{
    public TextMeshProUGUI text;
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(Display), RpcTarget.Others, photonView.OwnerActorNr);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void Display(int player)
    {
        text.SetText(text.text + Environment.NewLine + $"Player {player} collected a gem");
    }
}
