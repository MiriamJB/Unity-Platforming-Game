// Sign.cs
using UnityEngine;

public class Sign : MonoBehaviour
{
    private TutorialText tutorialText;

    private void Start()
    {
        tutorialText = FindObjectOfType<TutorialText>();
        if (tutorialText == null)
        {
            Debug.LogError("TutorialText object not found in the scene!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialText != null)
            {
                tutorialText.SetTutorialTextVisibility(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tutorialText != null)
            {
                tutorialText.SetTutorialTextVisibility(false);
            }
        }
    }
}
