using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public string newGameLevel = "Level/1";
    public string ControlsScene = "Controls";
    public string SettingsScene = "Settings";
    public string MainMenuScene = "Main Menu";

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
