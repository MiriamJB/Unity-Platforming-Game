using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    public Transform player;
    public float x,y,z;

    public int damage;
    public PlayerHealth playerHealth;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //player.position = new Vector3(x,y,z);
            //playerHealth.TakeDamage(damage);

            // Restart scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}
