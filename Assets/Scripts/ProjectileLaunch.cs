using UnityEngine;

public class ProjectileLaunch : MonoBehaviour {
    // Start is called before the first frame update
    public Player player; // Reference to the Player script

    public GameObject projectilePrefab;
    public Transform projectileSpawn;

    public float shootTime;
    public float shootCounter;
    public float spawnOffsetDistance = 1f; // Offset distance to prevent collision

    private float throwCounter = 0;
    private float throwTime = .6f;


    void Start() {
        shootCounter = shootTime;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("Fire2") && shootCounter <= 0) {
            // change throwing variables for the animator
            player.isShooting = true;
            throwCounter = throwTime;

            // Calculate offset based on player's facing direction
            Vector3 spawnOffset = player.isFacingRight ? new Vector3(spawnOffsetDistance, 0, 0) : new Vector3(-spawnOffsetDistance, 0, 0);
            Vector3 spawnPosition = projectileSpawn.position + spawnOffset;

            // Instantiate the projectile with the adjusted spawn position
            GameObject projectileObject = Instantiate(projectilePrefab, spawnPosition, projectileSpawn.rotation);
            Projectile projectileScript = projectileObject.GetComponent<Projectile>();

            // Set the projectile's direction based on the player's facing direction
            if (player.isFacingRight) {
                projectileScript.direction = Vector2.right;
            } else {
                projectileScript.direction = Vector2.left;
            }

            shootCounter = shootTime;
        }

        shootCounter -= Time.deltaTime;
        throwCounter -= Time.deltaTime;

        if (player.isShooting && throwCounter <= 0) {
            player.isShooting = false;
        }
        
    }
}
