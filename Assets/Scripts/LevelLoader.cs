using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void LoadTitleScene()
    {
        StartCoroutine(LoadScene("Main Menu"));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadScene("Scenes/Level/1"));
    }

    public void LoadNextLevel(int nextLevelIndex)
    {
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            StartCoroutine(LoadScene(nextLevelIndex));
        else
        {
            Debug.Log("No more levels available.");
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

    // if the first level then just restart, else load game over screen
    public void LoadGameOver()
    {
        // if level 1 just loop back
        if (SceneManager.GetActiveScene().name == "1")
            LoadGame();
        else
            StartCoroutine(LoadScene("Game Over", 1.0f));

        
    }

    // this version takes the scene name
    IEnumerator LoadScene(string sceneName, float delay = 0.75f)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // this version takes the scene index
    IEnumerator LoadScene(int sceneIndex, float delay = 0.75f)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
