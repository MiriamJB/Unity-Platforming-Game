using UnityEngine;

public class Rocket : MonoBehaviour {

    public Player player; // Define public definiton for player to reference.
    public UIManager uIManager;

    void OnTriggerEnter(Collider other) {
        // If Player collides with object, enable the victory screen
        if (other.gameObject.CompareTag("Player")) {
            uIManager.VictoryScreen(player);
        }
    }
}
