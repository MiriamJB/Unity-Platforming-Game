using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restart scene
        }
    }

}
