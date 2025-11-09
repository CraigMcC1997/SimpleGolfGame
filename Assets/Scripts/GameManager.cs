using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public BallManager ballManager;

    public SecondChance secondChanceMenu;

    public static bool allowSecondChance = true;

    public ParticleSystem holeInOneParticles;
    ParticleSystem holeInOneParticlesInstance;
    float ballSpeed;

    public GameObject holeinOneText;

    UpdateStats updateStats;

    const int NUM_MENUS = 4; // Main Menu and Game Over

    void Start()
    {
        holeinOneText.SetActive(false);
        updateStats = levelLoader.gameObject.GetComponent<UpdateStats>();
    }

    void Update()
    {
        // if player has ran out of shots then they have failed
        if (BallManager.shots_left <= 0 && ballManager.getVelocity() <= 0.0f)
        {
            BallManager.allowControl = false;

            // if on first level, just reload it to allow for faster retry
            if (SceneManager.GetActiveScene().name == "1")
                levelLoader.ReloadLevel(false);
            else
            {
                // for levels after the first, give option for second chance (only once per game)
                if (allowSecondChance)
                {
                    allowSecondChance = false;
                    secondChanceMenu.DisplaySecondChanceMenu();
                }
                else
                {
                    levelLoader.LoadGameOver();
                }
            }
        }
    }

    bool checkforHoleInOne()
    {
        if (BallManager.shots_left == 1)
        {
            holeinOneText.SetActive(true);
            return true;
        }
        return false;
        //!!TODO!! TMP disabled until I can progress more with partical systems
        //holeInOneParticlesInstance = Instantiate(holeInOneParticles, holeinOneText.transform.position, Quaternion.identity);
    }

    public void LoadNextLevel()
    {
        //check for hole in one
        bool isHoleInOne = checkforHoleInOne();

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentIndex + 1;
        updateStats.UpdateHighScore(nextLevel - NUM_MENUS); // removes menus from highscore
        updateStats.UpdateShotsTaken(BallManager.MAX_SHOTS - BallManager.shots_left);
        updateStats.UpdateLevelsCleared();

        levelLoader.LoadNextLevel(nextLevel, isHoleInOne);
    }
}
