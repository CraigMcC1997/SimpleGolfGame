using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    //main menu
    public void StartGameButton()
    {
        LevelLoader.Instance.LoadGame();
    }

    public void DisplayControlsButton()
    {
        LevelLoader.Instance.LoadControls();
    }

    public void OpenSettingsButton()
    {
        LevelLoader.Instance.LoadSettings();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        LevelLoader.Instance.LoadTitleScene();
    }
}
