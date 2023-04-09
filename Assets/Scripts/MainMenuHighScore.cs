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
        highScore_text.text = "High Score: " + PlayerPrefs.GetInt("highScore");
    }
}
