using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Define public definiton for player to reference.
    public Player player;

    void OnTriggerExit(Collider other) {
        // If Player collides with object, execute die script on player object.
        if (other.gameObject.CompareTag("Player")) {
            player.Die();
        }
    }

}
