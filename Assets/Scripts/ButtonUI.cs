using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public LevelLoader levelLoader;
    public void StartGameButton()
    {
        levelLoader.LoadGame();
    }

    public void DisplayControlsButton()
    {
        levelLoader.LoadControls();
    }

    public void OpenSettingsButton()
    {
        levelLoader.LoadSettings();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        levelLoader.LoadTitleScene();
    }
}
