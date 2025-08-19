using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

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
        StartCoroutine(LoadScene(nextLevelIndex));
    }

    public void LoadSettings()
    {
        StartCoroutine(LoadScene("Settings"));
    }

    public void LoadControls()
    {
        StartCoroutine(LoadScene("Controls"));
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadScene("Game Over", 1.0f));
    }

    IEnumerator LoadScene(string sceneName, float delay = 0.75f)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadScene(int sceneIndex, float delay = 0.75f)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
