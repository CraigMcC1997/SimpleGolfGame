using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.75f;

    UpdateStats updateStats;

    const int NUM_LEVELS = 5;

    void Start()
    {
        updateStats = gameObject.GetComponent<UpdateStats>();
    }

    public void LoadTitleScene(bool fromLevel = false)
    {
        if (fromLevel)
        {
            updateStats.UpdateHighScore(0);
            updateStats.UpdateShotsTaken(BallManager.MAX_SHOTS - BallManager.shots_left);
            updateStats.UpdateLevelsCleared(int.Parse(SceneManager.GetActiveScene().name) - 1);
            updateStats.UpdateFailedAttempts();
        }

        StartCoroutine(LoadScene("Main Menu"));
    }

    public void LoadGame()
    {
        updateStats.UpdateHighScore();
        StartCoroutine(LoadScene("Scenes/Level/1"));
    }

    public void ReloadLevel(bool secondChance = false)
    {
        if (!secondChance)
            updateStats.UpdateFailedAttempts();

        updateStats.UpdateShotsTaken(BallManager.MAX_SHOTS - BallManager.shots_left);

        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel(int nextLevel, bool isHoleInOne = false)
    {
        updateStats.UpdateHighScore(nextLevel);
        updateStats.UpdateShotsTaken(BallManager.MAX_SHOTS - BallManager.shots_left);

        // if there is another level, load it else load main menu
        if (nextLevel < NUM_LEVELS)
        {
            string nextLevelName = nextLevel.ToString();

            if (isHoleInOne)
                StartCoroutine(LoadScene(nextLevelName, 1.0f)); // TODO: tmp delay for x seconds to show text
            else
                StartCoroutine(LoadScene(nextLevelName));
        }
        else
        {
            Debug.Log("No more levels available.");
            updateStats.UpdateSuccessfulAttempts(); //TODO: THIS IS TEMP AS FINAL LEVEL NOT DONE, USING LAST AVAILABLE LEVEL AS TESTING, SHOULD BE MORE ROBUST
            StartCoroutine(LoadScene("Main Menu"));
        }

    }

    public void LoadSettings()
    {
        StartCoroutine(LoadScene("Settings"));
    }

    public void LoadControls()
    {
        StartCoroutine(LoadScene("Controls"));
    }

    public void LoadStats()
    {
        StartCoroutine(LoadScene("Stats"));
    }

    // if the first level then just restart, else load game over screen
    public void LoadGameOver(int MaxRoundReached)
    {
        updateStats.UpdateFailedAttempts();
        updateStats.UpdateShotsTaken(BallManager.MAX_SHOTS - BallManager.shots_left);
        updateStats.UpdateLevelsCleared(MaxRoundReached);

        StartCoroutine(LoadScene("Game Over", 1.0f));
    }

    // this version takes the scene name
    IEnumerator LoadScene(string sceneName, float delay = 0.75f)
    {
        if (delay != transitionTime)
        {
            yield return new WaitForSeconds(delay);
        }

        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
        SceneManager.LoadScene(sceneName);
    }

    // this version takes the scene index
    IEnumerator LoadScene(int sceneIndex, float delay = 0.75f)
    {
        if (delay != transitionTime)
        {
            yield return new WaitForSeconds(delay);
        }

        if (transition != null)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
