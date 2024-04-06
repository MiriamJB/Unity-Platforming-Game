using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject victoryUI;
    private Player player;
    public string nextScene;

    public void VictoryScreen(Player p) {
        victoryUI.SetActive(true);
        player = p;
        player.DisableMovement();
        Time.timeScale = 0;
    }

    public void ContinueButton() {
        victoryUI.SetActive(false);
        player.canMove = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

}
