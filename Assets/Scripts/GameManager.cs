using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject ball;
    BallManager ballManager;
    float ballSpeed;

    const int NUM_MENUS = 4; // Main Menu and Game Over

    void Start()
    {
        ballManager = ball.GetComponent<BallManager>();
    }

    void updateHighScore(int level)
    {
        if (PlayerPrefs.HasKey("highScore"))
            if (level > PlayerPrefs.GetInt("highScore"))
                PlayerPrefs.SetInt("highScore", level);
        else
            PlayerPrefs.SetInt("highScore", level);

        PlayerPrefs.Save();
    }

    void Update()
    {
        // if player has ran out of shots then they have failed
        if (BallManager.shots_left <= 0 && ballManager.getVelocity() <= 0.0f)
        {
            BallManager.allowControl = false;

            // if level 1 just loop back
            if (SceneManager.GetActiveScene().name == "1")
                levelLoader.LoadGame();
            else
                levelLoader.LoadGameOver();
        }
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentIndex + 1;
        updateHighScore(nextLevel - NUM_MENUS); // removes menus from highscore

        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            levelLoader.LoadNextLevel(nextLevel);
        }
        else
        {
            Debug.Log("No more levels available.");
            levelLoader.LoadTitleScene();
        }
    }
}
