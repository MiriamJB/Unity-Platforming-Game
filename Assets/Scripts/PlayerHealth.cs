using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth;
    public int health;
    public Player player;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;
    public Text gameOverText;
    private OxygenDisplay oxygenDisplay;
    public SkinnedMeshRenderer meshRender;
    private Color originalColor;


    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        oxygenDisplay = FindObjectOfType<OxygenDisplay>(); // Find and assign the OxygenDisplay component
        UpdateCameraUI(); // Start the coroutine to apply damage when oxygen level is -1
        originalColor = meshRender.material.color; // get the original color of the mesh
    }

    void UpdateCameraUI() {
        if (health <= 0) {
            gameOverText.text = "GAME OVER";
        }
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            player.Die();
            UpdateCameraUI();
        } else {
            // flash red to indicate taking damage
            changeColorToRed();
            Invoke(nameof(changeToOriginalColor), .2f);
            Invoke(nameof(changeColorToRed), .4f);
            Invoke(nameof(changeToOriginalColor), .6f);
            Invoke(nameof(changeColorToRed), .8f);
            Invoke(nameof(changeToOriginalColor), 1f);
        }
    }

    // Update the hearts display
    void UpdateHeartsDisplay() {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    void changeColorToRed() {
        meshRender.material.color = new Color(.9f, .2f, .2f, 1);
    }

    void changeToOriginalColor() {
        meshRender.material.color = originalColor;
    }
}
