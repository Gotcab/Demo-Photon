using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviourPun
{
    public float speed = 1;

    public PlayerMovementEvent OnPlayerMoved;

    private void Update()
    {
        if (!photonView.IsMine) return;

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(x, 0, z).normalized;
        if (moveVector != Vector3.zero)
        {
            var newPosition = transform.position + moveVector * speed * Time.deltaTime;
            OnPlayerMoved.Invoke(newPosition);
        }

    }
}

[System.Serializable]
public class PlayerMovementEvent : UnityEvent<Vector3> { }
