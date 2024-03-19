using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Define public definiton for player to reference.
    public Player player;
    // Define the scene that will be loaded when player collides with rocket.
    public string targetScene;

    void OnTriggerEnter(Collider other) {
        // If Player collides with object, load the target scene.
        if (other.gameObject.CompareTag("Player")) {
            SceneManager.LoadScene(targetScene);
        }
    }
}
