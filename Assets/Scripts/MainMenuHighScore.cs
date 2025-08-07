using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScore_text;

    // Start is a Unity callback function that is called when the script component is enabled and starts running.
    // It retrieves the high score from Player Preferences and updates the high score text if it is greater than 0.
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.SetInt("highScore", 0);

        int highscore = PlayerPrefs.GetInt("highScore");
        if (highscore > 0)
            highScore_text.text = "High Score: " + highscore;
    }
}
