using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Level/1";
    [SerializeField] private string ControlsScene = "Controls";
    [SerializeField] private string SettingsScene = "Settings";
    [SerializeField] private string MainMenuScene = "Main Menu";

    //main menu
    public void StartGameButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void DisplayControlsButton()
    {
        SceneManager.LoadScene(ControlsScene);
    }

    public void OpenSettingsButton()
    {
        SceneManager.LoadScene(SettingsScene);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
}
