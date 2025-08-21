using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public BallManager ballManager;

    public ParticleSystem holeInOneParticles;
    ParticleSystem holeInOneParticlesInstance;
    float ballSpeed;

    GameObject hole;

    const int NUM_MENUS = 4; // Main Menu and Game Over

    void Start()
    {
        hole = GameObject.Find("Hole in One");
        hole.SetActive(false);
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

            levelLoader.LoadGameOver();
        }
    }

    void checkforHoleInOne()
    {
        if (BallManager.shots_left == 1)
        {
            hole.SetActive(true);
            //TMP disabled until I can progress more with partical systems
            //holeInOneParticlesInstance = Instantiate(holeInOneParticles, hole.transform.position, Quaternion.identity);
        }
    }

    public void LoadNextLevel()
    {
        //check for hole in one
        checkforHoleInOne();

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentIndex + 1;
        updateHighScore(nextLevel - NUM_MENUS); // removes menus from highscore

        levelLoader.LoadNextLevel(nextLevel);
    }
}
