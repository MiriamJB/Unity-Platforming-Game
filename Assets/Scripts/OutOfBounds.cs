using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    public Player player;

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restart scene
            player.Die();
        }
    }

}
