using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    private Text tutorialTextComponent;

    private void Start()
    {
        tutorialTextComponent = GetComponent<Text>();
        SetTutorialTextVisibility(false); // Hide the tutorial text initially
    }

    public void SetTutorialTextVisibility(bool isVisible)
    {
        tutorialTextComponent.enabled = isVisible;
    }
}