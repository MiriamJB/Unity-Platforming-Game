using UnityEngine;

public class AirBubbleCollector : MonoBehaviour {

    private AirBubbleDisplay bubbleDisplay;
    private int bubblesCollected = 0;

    void Start () {
        bubbleDisplay = GameObject.FindObjectOfType<AirBubbleDisplay>();
    }

    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("AirBubble")) {
            AirBubble airBubble = other.GetComponent<AirBubble>();
            if (airBubble != null) {
                bubblesCollected++;
                bubbleDisplay.UpdateBubbleCount(bubblesCollected);
                Destroy(other.gameObject);
            }
        }
    }
}
