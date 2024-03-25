using UnityEngine;

public class AirBubble : MonoBehaviour {
    public float verticalSpeed = 1.5f;
    public float amplitude = 0.4f;

    private float startY;

    // Start is called before the first frame update
    void Start() {
        startY = transform.position.y; // Store the initial Y position
    }

    // Update is called once per frame
    void Update() {
        // Moving up and down
        float newY = startY + Mathf.Sin(Time.time * verticalSpeed) * amplitude; // Adjust the Y position using the amplitude
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Method for collider
    void OnTriggerEnter(Collider other) {
        // Checking if the colliding object is the player
        if (other.CompareTag("Player")) {
            // Destroy the air bubble when the player touches it
            Destroy(gameObject);

            // Accessing the OxygenDisplay script attached to the oxygen display object
            OxygenDisplay oxygenDisplay = FindObjectOfType<OxygenDisplay>();

            // If the OxygenDisplay script is found
            if (oxygenDisplay != null) {
                // Increase the oxygen level in the oxygen display
                oxygenDisplay.IncreaseOxygenLevel();
            } else {
                Debug.LogWarning("OxygenDisplay script not found!");
            }
        }
    }
}
