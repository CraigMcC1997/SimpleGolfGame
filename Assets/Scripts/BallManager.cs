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
    public bool onUIElement = false;

    Rigidbody2D Ball_rb;
    public DragAndShoot dragScript; // public to allow UI to get required info

    // Start is a Unity callback function that is called when the script is first enabled or the object is instantiated.
    // It initializes the dragScript, Ball_rb, originalPos, and originalVelocity variables with respective component references and values.
    void Start()
    {
        dragScript = GetComponent<DragAndShoot>();
        Ball_rb = GetComponent<Rigidbody2D>();

        originalPos = gameObject.transform.position;
        originalVelocity = Ball_rb.velocity;
    }

    // KeepBallOnScreen is a custom function that ensures the ball remains within the visible screen area.
    // It uses the WorldToScreenPoint method of the Camera class to convert the ball's world position to screen coordinates.
    // If the ball goes off-screen horizontally, it resets its position and velocity to the original values.
    // Note: This function should be called periodically, e.g., in the Update() function or via a coroutine, to keep the ball on screen.
    void KeepBallOnScreen()
    {
        Vector2 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (playerPosScreen.x > Screen.width || playerPosScreen.x < 0.0f)
        {
            Ball_rb.transform.position = originalPos;
            Ball_rb.velocity = originalVelocity;
        }
    }

    // loadGameOver is a custom function that loads the appropriate scene for game over based on the current active scene.
    // If the current active scene is named "1", it reloads the same scene to restart the game.
    // Otherwise, it loads the "Game Over" scene to display the game over screen.
    // Note: This function should be called when the game over condition is met, such as when the player loses all their lives or fails to achieve the game's objective.
    void loadGameOver()
    {
        Scene scene = SceneManager.GetActiveScene();

        // if level 1 just loop back
        if (scene.name == "1")
            SceneManager.LoadScene(scene.name);
        else
            SceneManager.LoadScene("Game Over");
    }

    // updateHighScore is a custom function that updates the high score in PlayerPrefs based on the current level.
    // If the PlayerPrefs already contains a high score, it compares it with the current level and updates it if necessary.
    // Otherwise, it sets the current level as the initial high score.
    // Note: This function should be called after completing a level and determining the player's score or progress.
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

    // Update is a Unity callback function that is called every frame. It contains the game logic for controlling player input,
    // checking game over condition, and keeping the ball on screen.
    // If the player runs out of shots (dragScript.shots_left == 0), it stops player control and invokes the loadGameOver() function
    // after a delay of 5 seconds to display the game over screen.
    // It also checks the Time.timeScale to determine if the game is paused (Time.timeScale == 0), and disables player control accordingly.
    // Lastly, it calls the KeepBallOnScreen() function to ensure the ball stays within the screen bounds.
    void Update()
    {
        // stop players controll and show death screen
        if (dragScript.shots_left == 0)
        {
            allowControl = false;

            //start count down before displaying game over screen
            Invoke("loadGameOver", 5);
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

    // LoadNextLevel is a function that is called when the player collides with the "End Flag" object.
    // It checks if the collided object has a tag "End Flag" (other.gameObject.tag == "End Flag").
    // If so, it retrieves the current active scene's build index using SceneManager.GetActiveScene().buildIndex + 1
    // and saves the current level as the highest reached level by calling updateHighScore() function with the appropriate level value.
    // Then, it loads the next level using SceneManager.LoadScene(nextLevel_i).
    // Note: The function assumes that the build order of the scenes is correct, so the next level can be loaded directly using its build index.
    // Alternatively, you can use SceneUtility.GetBuildIndexByScenePath() to check if the next scene exists by passing its name or path as a string,
    // and load it accordingly.
    void LoadNextLevel()
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