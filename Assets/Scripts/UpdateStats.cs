using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpdateStats : MonoBehaviour
{
    public void UpdateHighScore(int levelCompleted = 1)
    {
        int highScore = PlayerPrefs.GetInt("highScore", 0);
        if (levelCompleted > highScore)
        {
            PlayerPrefs.SetInt("highScore", levelCompleted);
        }
    }

    public void UpdateShotsTaken(int shots)
    {
        int totalShots = PlayerPrefs.GetInt("TotalShots", 0);
        totalShots += shots;
        PlayerPrefs.SetInt("TotalShots", totalShots);
    }

    public void UpdateLevelsCleared()
    {
        int totalLevelsCleared = PlayerPrefs.GetInt("TotalLevelsCleared", 0);
        totalLevelsCleared += 1;
        PlayerPrefs.SetInt("TotalLevelsCleared", totalLevelsCleared);
    }

    public void UpdateFailedAttempts()
    {
        int totalFailedAttempts = PlayerPrefs.GetInt("TotalFailedAttempts", 0);
        totalFailedAttempts += 1;
        PlayerPrefs.SetInt("TotalFailedAttempts", totalFailedAttempts);
    }

    public void UpdateSuccessfulAttempts()
    {
        int successfulAttempts = PlayerPrefs.GetInt("SuccessfulAttempts", 0);
        successfulAttempts += 1;
        PlayerPrefs.SetInt("SuccessfulAttempts", successfulAttempts);
    }
}
