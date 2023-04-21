using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;

    // Start is a Unity callback function that is called when the script is first enabled or the object is instantiated.
    // It sets the "pauseMenuUI" game object to be inactive when the script starts, effectively hiding the pause menu.
    // Note: This function is automatically called by Unity and does not need to be manually invoked.
    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    // Update is a Unity callback function that is called once per frame.
    // It checks for the "Escape" key being pressed, and if so, it toggles between pausing and resuming the game.
    // It calls the Pause() function if the game is not currently paused, and calls the Resume() function if the game is paused.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pause is a public function that is called to pause the game.
    // It activates the pause menu UI, sets the time scale to 0 (pausing the game),
    // and sets a flag to indicate that the game is paused.
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GamePaused = true;
    }

    // Resume is a public function that is called to resume the game after it has been paused.
    // It deactivates the pause menu UI, sets the time scale back to 1 (resuming the game),
    // and sets a flag to indicate that the game is no longer paused.
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    // LoadMainMenu is a public function that is called to load the main menu scene.
    // It sets the time scale back to 1 (resuming the game if paused), resets the game paused flag,
    // and loads the "Main Menu" scene using the SceneManager class.
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GamePaused = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {

    }
}
