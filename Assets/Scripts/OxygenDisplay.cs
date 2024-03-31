using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OxygenDisplay : MonoBehaviour
{
    public Sprite empty;
    public Sprite full;
    public Image[] oxygenImages;
    public Image[] hearts; 
    public PlayerHealth playerHealth;

    public int currentOxygenIndex = 4;

    // How much time in seconds it takes to lose oxygen/health.
    public float oxygenTick = 4f;


    void Start()
    {
        UpdateOxygenDisplay();
        StartCoroutine(DecreaseOxygenAndLoseHealth());
    }

    IEnumerator DecreaseOxygenAndLoseHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(oxygenTick); 
            if (currentOxygenIndex >= 0)
            {
                currentOxygenIndex--;
                UpdateOxygenDisplay();
            }
            else
            {
                playerHealth.TakeDamage(1);
                UpdateHeartsDisplay();
            }
        }
    }


    // Increase the oxygen level
    public void IncreaseOxygenLevel()
    {
        if (currentOxygenIndex < oxygenImages.Length - 1)
        {
            currentOxygenIndex++; // Increase the oxygen level
            UpdateOxygenDisplay(); // Update the oxygen display
        }
    }

    // Update the oxygen display
    void UpdateOxygenDisplay()
    {
        for (int i = 0; i < oxygenImages.Length; i++)
        {
            if (i > currentOxygenIndex)
            {
                oxygenImages[i].sprite = empty;
            }
            else
            {
                oxygenImages[i].sprite = full;
            }
        }
    }

    // Update the hearts display
    void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.health)
            {
                hearts[i].sprite = full;
            }
            else
            {
                hearts[i].sprite = empty;
            }
        }
    }
}
