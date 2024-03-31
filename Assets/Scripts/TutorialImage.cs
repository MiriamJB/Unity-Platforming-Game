using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialImage : MonoBehaviour
{
    public GameObject tutorialImagePrefab;
    private GameObject tutorialImageInstance;

    private Camera mainCamera;

    private void Start()
    {
        // Get a reference to the main camera
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialImageInstance == null)
            {
                // Calculate the position of the image in the center of the screen
                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
                Vector3 worldCenter = mainCamera.ScreenToWorldPoint(screenCenter);
                worldCenter.z = 0f;

                // Instantiate the tutorial image prefab at the calculated position
                tutorialImageInstance = Instantiate(tutorialImagePrefab, worldCenter, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialImageInstance != null)
            {
                // Destroy the tutorial image instance when the player exits the trigger zone
                Destroy(tutorialImageInstance);
            }
        }
    }
}
