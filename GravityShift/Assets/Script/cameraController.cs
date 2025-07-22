using UnityEngine;

public class cameraController : MonoBehaviour
{
     public Transform player;
    public Vector3 offset;

    public float smoothedSpeed;

    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition,smoothedSpeed);
        transform.position = smoothedPosition;
    }
}
