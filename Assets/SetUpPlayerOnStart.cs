using Photon.Pun;
using UnityEngine;

public class SetUpPlayerOnStart : MonoBehaviourPun
{
    public MeshRenderer renderer;

    private void Start()
    {
        object[] data = photonView.InstantiationData;
        Color playerColor = new Color((float)data[0], (float)data[1], (float)data[2]);
        renderer.material.color = playerColor;
    }
}
