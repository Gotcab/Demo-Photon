using Photon.Pun;
using UnityEngine;

public class GoOfflineOnStart : MonoBehaviour
{
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        if (enabled)
        {
            PhotonNetwork.OfflineMode = true;
        }
    }
}
