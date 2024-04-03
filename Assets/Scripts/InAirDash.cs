using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InAirDash : MonoBehaviour {
    private Player player; // Reference to the Player class

    // Customize these variables according to your needs
    [SerializeField] private int numDashes = 5;
    [SerializeField] private int dashForce = 15;
    [SerializeField] private float dashCooldown = 5f;

    private int dashesLeft;
    private float lastDashTime;
    public Slider cooldownSlider; // Reference to the UI Slider for cooldown

    void Start() {
        player = GetComponent<Player>();
        dashesLeft = numDashes;
        cooldownSlider.value = 0f; // Set initial fill amount to 0
    }

    void Update() {
        if (player.inAir && !player.isDashing && dashesLeft > 0 && Time.time - lastDashTime >= dashCooldown) {
            if (Input.GetKeyDown(KeyCode.LeftShift)) { // You can change this to any other input 
                StartCoroutine(Dash());
            }
        }

        // Update cooldown slider fill amount
        float timeSinceLastDash = Time.time - lastDashTime;
        float cooldownPercentage = Mathf.Clamp01(timeSinceLastDash / dashCooldown);
        cooldownSlider.value = cooldownPercentage;
    }

    IEnumerator Dash() {
        player.isDashing = true;
        player.canMove = false;

        Vector3 dir = new Vector3(player.horizontalInput, 0, 0); // Adjust this based on your movement input
        player.rigidBodyComponent.velocity = Vector3.zero;
        player.rigidBodyComponent.AddForce(dir.normalized * dashForce, ForceMode.VelocityChange);
        dashesLeft--;

        yield return new WaitForSeconds(0.2f);

        player.isDashing = false;
        player.canMove = true;
        lastDashTime = Time.time;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DashPickup")) { // Change this to the tag of the object that provides extra dashes
            dashesLeft += 1; // Increase the number of available dashes
            Destroy(other.gameObject); // Assuming the pickup is a consumable
        }
    }
}
