using System.Collections;
using UnityEngine;

public class InAirDash : MonoBehaviour
{
    private Player player; // Reference to the Player class
    private bool isDashing = false;

    // Customize these variables according to your needs
    [SerializeField] private int numDashes;
    [SerializeField] private int dashForce;
    [SerializeField] private float dashCooldown;

    private int dashesLeft;
    private float lastDashTime;

    void Start()
    {
        player = GetComponent<Player>();
        dashesLeft = numDashes;
    }

    void Update()
    {
        if (player.inAir && !isDashing && dashesLeft > 0 && Time.time - lastDashTime >= dashCooldown)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) // You can change this to any other input
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        player.canMove = false;

        Debug.Log("Dashing!"); // Log that the player is dashing

        Vector3 dir = new Vector3(player.horizontalInput, 0, 0); // Adjust this based on your movement input
        player.rigidBodyComponent.velocity = Vector3.zero;
        player.rigidBodyComponent.AddForce(dir.normalized * dashForce, ForceMode.VelocityChange);
        dashesLeft--;

        yield return new WaitForSeconds(0.2f);

        isDashing = false;
        player.canMove = true;
        lastDashTime = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DashPickup")) // Change this to the tag of the object that provides extra dashes
        {
            dashesLeft += 1; // Increase the number of available dashes
            Destroy(other.gameObject); // Assuming the pickup is a consumable
        }
    }
}
