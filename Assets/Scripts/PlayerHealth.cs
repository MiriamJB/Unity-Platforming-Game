using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){
            //Destroy(gameObject);
            player.canMove = false;
            player.isDead = true;
        }
    }
}
