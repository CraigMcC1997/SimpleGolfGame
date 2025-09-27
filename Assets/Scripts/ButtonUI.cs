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

    public void DisplayStatsButton()
    {
        levelLoader.LoadStats();
    }

    public void ReturnToMenu()
    {
        levelLoader.LoadTitleScene(false);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
