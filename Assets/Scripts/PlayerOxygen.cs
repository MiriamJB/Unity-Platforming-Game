using UnityEngine;

public class PlayerOxygen : MonoBehaviour
{
    public int maxOxygen;
    public int oxygen;
    public Player player;

    void Start()
    {
        oxygen = maxOxygen;
    }

    // Method to increase the oxygen level
    public void IncreaseOxygenLevel()
    {
        
        if (oxygen < maxOxygen)
        {
            oxygen++; // Increment the oxygen level
            Debug.Log("Oxygen increased: " + oxygen); 
        }

    }
}
