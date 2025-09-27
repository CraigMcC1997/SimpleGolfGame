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
    bool loadGameOverBool = true;

    void Start()
    {
        loadGameOverBool = true;
        updateStats = gameObject.GetComponent<UpdateStats>();
    }

    public void LoadTitleScene()
    {
        StartCoroutine(LoadScene("Main Menu"));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadScene("Scenes/Level/1"));
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel(int nextLevelIndex, bool isHoleInOne)
    {
        // if there is another level, load it else load main menu
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (isHoleInOne)
                StartCoroutine(LoadScene(nextLevelIndex, 1.0f)); // TODO: tmp delay for x seconds to show text
            else
                StartCoroutine(LoadScene(nextLevelIndex));
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
    public void LoadGameOver()
    {
        if (loadGameOverBool)
        {
            loadGameOverBool = false;
            updateStats.UpdateFailedAttempts();
            updateStats.UpdateShotsTaken(BallManager.MAX_SHOTS - BallManager.shots_left);
        }

        // if level 1 just loop back
        if (SceneManager.GetActiveScene().name == "1")
            ReloadLevel();
        else
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
