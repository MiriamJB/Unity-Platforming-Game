using UnityEngine;
using UnityEngine.UI;

    public class AirBubbleDisplay : MonoBehaviour {

        public Text bubbleCountText;
        private int bubblesCollected = 0;
        

    public void UpdateBubbleCount (int Collected) {
        bubblesCollected += Collected;
        bubbleCountText.text = "Bubbles collected: " + bubblesCollected;
    }
}