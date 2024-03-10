using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Player player;
    private OxygenDisplay oxygenDisplay;

    void Start()
    {
        health = maxHealth;


        oxygenDisplay = FindObjectOfType<OxygenDisplay>();
    }

    void Update()
    {
        //if oxygen level has reached 0
        if (oxygenDisplay != null && oxygenDisplay.currentOxygenIndex == 0)
        {
            // Start taking damage every two seconds
            StartCoroutine(TakeDamageOverTime());
        }
    }

    // Coroutine to apply damage every two seconds
    IEnumerator TakeDamageOverTime()
    {
        while (oxygenDisplay.currentOxygenIndex == 0)
        {
            // Wait for two seconds
            yield return new WaitForSeconds(2f);

            // Apply damage
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (player.invulnerability == false)
        {
            health -= damage;
            player.ActivateInvulnerability();
            if (health <= 0)
            {
                //Destroy(gameObject);
                player.canMove = false;
                player.isDead = true;
            }
        }
    }
}
