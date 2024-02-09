using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    // public SpriteRenderer playerSr;
    // public Player jumpKeyWasPressed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){
            Destroy(gameObject);
            // playerSr.enabled = false;
            // jumpKeyWasPressed.enabled = false;
        }

    }


}
