using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator fadeOut;
    public Animator fadeIn;
    public static LevelLoader Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Trigger fade-in after scene loads
        fadeIn.SetTrigger("End");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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

    // this version takes the scene name
    IEnumerator LoadScene(string sceneName, float delay = 0.75f)
    {
        fadeOut.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // this version takes the scene index
    IEnumerator LoadScene(int sceneIndex, float delay = 0.75f)
    {
        fadeOut.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
