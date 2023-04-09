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
        //commented out, used for resetting high score
        //PlayerPrefs.SetInt("highScore", 0);

        //for round count
        int highscore = PlayerPrefs.GetInt("highScore");
        if (highscore > 0)
            highScore_text.text = "High Score: " + highscore;
    }
}
