using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public float speed;

    public float projectileLife;
    public float projectileCount;
    public Vector2 direction = Vector2.right; // Default direction is right
    public int damage; // Damage the projectile does
    public EnemyHealth enemyHealth; // Reference to the enemy health script

    void Start()
    {
        projectileCount = projectileLife;
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount -= Time.deltaTime;
        if(projectileCount <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * direction.x, rb.velocity.y);
    }
     private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>(); // Get the enemy health script
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Call the TakeDamage function in the enemy health script
            }
        }

        // Destroy the projectile after it hits something
        Destroy(gameObject);
    }
}

