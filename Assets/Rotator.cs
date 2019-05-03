using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotation = new Vector3(15, 30, 45);
    public float speed = 1;

    private void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
