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

    bool GameOver = false;

    void Start()
    {
        holeinOneText.SetActive(false);
    }

    void Update()
    {
        CheckForGameOver();
    }

    void CheckForGameOver()
    {
        // if player has ran out of shots then they have failed
        if (BallManager.shots_left <= 0 && ballManager.getVelocity() <= 0.0f && !GameOver)
        {
            GameOver = true;
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
                    //if this round failed then count previous as highest round cleared
                    int MaxRoundCleared = int.Parse(SceneManager.GetActiveScene().name) - 1;
                    levelLoader.LoadGameOver(MaxRoundCleared);
                }
            }
        }
    }

    bool checkforHoleInOne()
    {
        // allow for hole in one if its the first shot
        // allows for extras shots in future implementation
        if ((BallManager.MAX_SHOTS - 1) == BallManager.shots_left)
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

        // get next level index from current level name
        int nextLevel = int.Parse(SceneManager.GetActiveScene().name) + 1;

        levelLoader.LoadNextLevel(nextLevel, isHoleInOne);
    }
}
