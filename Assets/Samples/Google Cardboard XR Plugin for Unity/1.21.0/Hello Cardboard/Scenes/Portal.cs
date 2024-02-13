using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination; // Reference to the destination transform

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has a tag named "Player"
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        player.position = destination.position;
        // You might want to add some additional logic here, like rotating the player to match the destination's rotation.
    }
}
