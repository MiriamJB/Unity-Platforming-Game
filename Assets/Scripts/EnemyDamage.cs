using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public PlayerHealth playerHealth;
    private float knockback = 5.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
            playerHealth.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * knockback, ForceMode.VelocityChange);
            playerHealth.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * knockback, ForceMode.VelocityChange);
        }
    }
}
