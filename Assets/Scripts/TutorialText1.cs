using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText1 : MonoBehaviour
{
    public GameObject tutorialTextPrefab;
    private GameObject tutorialTextInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialTextInstance == null)
            {
                // Instantiate the tutorial text prefab and adjust its position
                Vector3 newPosition = transform.position + Vector3.up * 2f;
                tutorialTextInstance = Instantiate(tutorialTextPrefab, newPosition, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialTextInstance != null)
            {
                // Destroy the tutorial text instance when the player exits the trigger zone
                Destroy(tutorialTextInstance);
            }
        }
    }
}

