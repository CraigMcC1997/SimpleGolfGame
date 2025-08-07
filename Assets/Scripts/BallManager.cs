using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BallManager : MonoBehaviour
{
    Vector3 startingPos;
    Vector3 originalVelocity;

    const int MAX_TURNS = 2;
    const int NUM_MENUS = 4; // Main Menu and Game Over
    public static bool allowControl = true;
    public static bool onUIElement = false;

    Rigidbody2D Ball_rb;
    public DragAndShoot dragScript; // public to allow UI to get required info

    void Start()
    {
        dragScript = GetComponent<DragAndShoot>();
        Ball_rb = GetComponent<Rigidbody2D>();

        startingPos = gameObject.transform.position;
        originalVelocity = Ball_rb.linearVelocity;
    }

    void KeepBallOnScreen()
    {
        Vector2 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (playerPosScreen.x > Screen.width || playerPosScreen.x < 0.0f)
        {
            Ball_rb.transform.position = startingPos;
            Ball_rb.linearVelocity = originalVelocity;
        }
    }

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
            if (level > PlayerPrefs.GetInt("highScore"))
                PlayerPrefs.SetInt("highScore", level);
        else
            PlayerPrefs.SetInt("highScore", level);

        PlayerPrefs.Save();

        Debug.Log("current level: " + level);
        Debug.Log("high score: " + PlayerPrefs.GetInt("highScore"));
    }

    void Update()
    {
        // stop players control and show death screen
        if (dragScript.shots_left == 0)
        {
            allowControl = false;

            // start count down before displaying game over screen
            Invoke("loadGameOver", 3f);
        }

        if (Time.timeScale == 0)
        {
            allowControl = false;
        }
        else
        {
            allowControl = true;
        }

        // keep ball on screen
        KeepBallOnScreen();
    }


    void LoadNextLevel()
    {
        int nextLevel_i = SceneManager.GetActiveScene().buildIndex + 1;

        //save the current level as highest reached
        updateHighScore(nextLevel_i - NUM_MENUS);

        //load the next level
        //SceneManager.LoadScene(nextLevel_i);

        string nextLevel = nextLevel_i.ToString();
        int nextLevelExists = SceneUtility.GetBuildIndexByScenePath(nextLevel);

        if (nextLevelExists > -1)
        {
           //load next scene assuming build order is correct
           SceneManager.LoadScene(nextLevel_i);
        }
        else
        {
           //reached end, load Main Menu
           SceneManager.LoadScene("Main Menu");
        }

    }

    // OnCollisionEnter2D is a Unity callback function that is called when a collision occurs with 2D colliders.
    // In this case, it is used to handle collision events with other objects.
    // It takes a Collision2D parameter "other" which represents the collision information of the other object involved in the collision.
    // The function calls the LoadNextLevel() function and passes the "other" parameter to it, which then checks if the collided object
    // has a tag "End Flag" and loads the next level accordingly.
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "End Flag")
        {
            LoadNextLevel();
        }
    }
}