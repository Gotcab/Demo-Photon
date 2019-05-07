using Photon.Realtime;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Color playerColor;

    public bool IsAvailable { get => owner != null; }

    private int owner = 0;

    public void Use(int playerId)
    {
        owner = playerId;
    }

    public void Release(int playerId)
    {
        if (playerId == owner)
        {
            owner = 0;
        }
        else
        {
            Debug.LogError($"PlayerId: {playerId} tried to release a SpawnPoint who is not his");
        }
    }
    
}
