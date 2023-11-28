using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player's transform
    public Vector3 offset; // The offset distance between the player and camera

    void Update()
    {
        // Update the camera's position to follow the player
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
}