using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform player; // The player's Transform
    [SerializeField] private float yOffset = 2f; // Offset to keep the camera slightly above the player
    [SerializeField] private float smoothSpeed = 0.125f; // Speed of smoothing for camera movement

    void LateUpdate()
    {
        // Check if the player is instantiated
        if (player == null)
        {
            // Try to find the player in the scene dynamically
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                return; // Exit if player is still not found
            }
        }

        // Compute the target position for the camera
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);

        // Smoothly interpolate the camera's position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
