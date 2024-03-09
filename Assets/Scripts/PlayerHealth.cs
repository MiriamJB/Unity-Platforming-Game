using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth;
    public int health;
    public Player player;

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
    }
    
    public void TakeDamage(int damage) {
        if (player.invulnerability == false) {
            health -= damage;
            player.ActivateInvulnerability();
            if (health <= 0) {
                player.Die();
            }
        }
    }
}
