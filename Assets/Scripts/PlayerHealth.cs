using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public Player player;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;
    public Text gameOverText;
    private OxygenDisplay oxygenDisplay;
    private bool startDeductingHearts = false; // Flag to indicate if hearts deduction should start
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        // Find and assign the OxygenDisplay component
        oxygenDisplay = FindObjectOfType<OxygenDisplay>();

        // Start the coroutine to apply damage when oxygen level is -1
        StartCoroutine(TakeDamageOverTime());
        UpdateCameraUI();
    }

    void UpdateCameraUI() {
        if (health <= 0) {
            gameOverText.text = "Game Over";
        }
    }

    // Coroutine to apply damage when oxygen level is -1
    IEnumerator TakeDamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for two seconds before each deduction check

            // Check if oxygen index is -1 and start deducting hearts
            if (oxygenDisplay.currentOxygenIndex <= 0)
            {
                startDeductingHearts = true;
            } else {
                startDeductingHearts = false;
            }

            // Deduct hearts if the flag is set
            if (startDeductingHearts)
            {
                TakeDamage(1); // Deduct 1 heart
                UpdateHeartsDisplay(); // Update the hearts display
            }
        }
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //Destroy(gameObject);
            //player.canMove = false;
            //player.isDead = true;
            player.Die();
            UpdateCameraUI();
        }
    }

    // Update the hearts display
    void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
