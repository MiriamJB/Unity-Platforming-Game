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

    public int damage;
    public EnemyHealth enemyHealth;

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
        // Check if the collision is with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Destroy the projectile after it hits something
        Destroy(gameObject);
    }
}
