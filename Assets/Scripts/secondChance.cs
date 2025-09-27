using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondChance : MonoBehaviour
{
    public GameObject secondChanceMenu;
    public LevelLoader levelLoader;

    // make sure menu is not shown at start
    void Start()
    {
        secondChanceMenu.SetActive(false);
    }

    // level over, show menu to give player a second chance
    public void DisplaySecondChanceMenu()
    {
        secondChanceMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitToMenu()
    {
        secondChanceMenu.SetActive(false);
        Time.timeScale = 1f;
        levelLoader.LoadTitleScene(true);
    }

    // if player loses, give option to retry once per game
    public void RetryLevel()
    {
        secondChanceMenu.SetActive(false);
        Time.timeScale = 1f;
        levelLoader.ReloadLevel(true);
    }
}
