using UnityEngine;

public class AirBubble : MonoBehaviour
{
    public float verticalSpeed = 1.5f;
    public float amplitude = 0.4f;

    private float startY;

    void Start()
    {
        startY = transform.position.y; // Store the initial Y position
    }

    // Method being called
    void Update()
    {
        // Moving up and down 
        float newY = startY + Mathf.Sin(Time.time * verticalSpeed) * amplitude; // Adjust the Y position using the amplitude
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Method for collider
    void OnTriggerEnter(Collider other)
    {
        // Checking if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Destroying the air bubble when the player touches it
            Destroy(gameObject);
        }
    }
}
