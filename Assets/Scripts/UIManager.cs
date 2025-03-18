using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button restartButton; // Reference to the restart button

    void Start()
    {
        // Hide the button at the start of the game
        restartButton.gameObject.SetActive(false);

        // Add a listener to the button so it calls RestartGame() when clicked
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowRestartButton()
    {
        // Show the button when the player dies
        restartButton.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}