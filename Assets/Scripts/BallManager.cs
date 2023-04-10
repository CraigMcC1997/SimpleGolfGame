using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BallManager : MonoBehaviour
{
    Vector3 originalPos;
    Vector3 originalVelocity;

    const int MAX_TURNS = 2;
    public bool allowControl = true;

    Rigidbody2D Ball_rb;
    Renderer renderer;
    public DragAndShoot dragScript; // public to allow UI to get required info

    void Start()
    {
        dragScript = GetComponent<DragAndShoot>();
        Ball_rb = GetComponent<Rigidbody2D>();

        originalPos = gameObject.transform.position;
        originalVelocity = Ball_rb.velocity;
        renderer = GetComponent<Renderer>();
    }

    void KeepBallOnScreen()
    {
        Vector2 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (playerPosScreen.x > Screen.width || playerPosScreen.x < 0.0f)
        {
            Ball_rb.transform.position = originalPos;
            Ball_rb.velocity = originalVelocity;
        }
    }

    //temp to allow invoke to wait x seconds before calling game over
    void loadGameOver()
    {
        Scene scene = SceneManager.GetActiveScene();

        // if level 1 just loop back
        if (scene.name == "1")
            SceneManager.LoadScene(scene.name);
        else
            SceneManager.LoadScene("Game Over");
    }

    void updateHighScore(int level)
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            if (level > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", level);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", level);
            PlayerPrefs.Save();
        }

        Debug.Log(level);
    }

    void Update()
    {
        // stop players controll and show death screen
        if (dragScript.shots_left == 0)
        {
            allowControl = false;

            //start count down before displaying game over screen
            Invoke("loadGameOver", 5);
        }

        // keep ball on screen
        KeepBallOnScreen();
    }

    void LoadNextLevel(Collision2D other)
    {
        if (other.gameObject.tag == "End Flag")
        {
            int nextLevel_i = SceneManager.GetActiveScene().buildIndex + 1;

            //save the current level as highest reached
            updateHighScore(nextLevel_i - 4);

            //load the next level
            SceneManager.LoadScene(nextLevel_i);

            //string nextLevel = nextLevel_i.ToString();
            //int nextLevelExists = SceneUtility.GetBuildIndexByScenePath(nextLevel);

            //if (nextLevelExists > -1)
            //{
            //    //load next scene assuming build order is correct
            //    SceneManager.LoadScene(nextLevel_i);
            //}
            //else
            //{
            //    //reached end, load Main Menu
            //    SceneManager.LoadScene("Main Menu");
            //}
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        LoadNextLevel(other);
    }
}