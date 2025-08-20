using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScore_text;

    void Start()
    {
        if (Input.GetKeyDown(KeyCode.R))
            PlayerPrefs.SetInt("highScore", 0);

        int highscore = PlayerPrefs.GetInt("highScore");
        if (highscore > 0)
            highScore_text.text = "High Score: " + highscore;
    }
}
