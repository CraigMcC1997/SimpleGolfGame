using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StatsPage_Manager : MonoBehaviour
{
    public TMP_Text highestLevelText;
    public TMP_Text totalShotsText;
    public TMP_Text totalLevelsClearedText;
    public TMP_Text totalFailedAttemptsText;
    public TMP_Text successfulAttemptsText;

    void Start()
    {
        // Initialize the round text when the game starts
        UpdateStatsText();
    }

    public void UpdateStatsText()
    {
        // Update the stats text based on PlayerPrefs values
        highestLevelText.text = "Highest Level: " + PlayerPrefs.GetInt("highScore", 1);
        totalShotsText.text = "Total Shots: " + PlayerPrefs.GetInt("TotalShots", 0);
        totalLevelsClearedText.text = "Total Levels Cleared: " + PlayerPrefs.GetInt("TotalLevelsCleared", 0);
        totalFailedAttemptsText.text = "Total Failed Attempts: " + PlayerPrefs.GetInt("TotalFailedAttempts", 0);
        successfulAttemptsText.text = "Successful Attempts: " + PlayerPrefs.GetInt("SuccessfulAttempts", 0);
    }

    public void ResetAllStatics()
    {
        PlayerPrefs.SetInt("highScore", 1);
        PlayerPrefs.SetInt("TotalShots", 0);
        PlayerPrefs.SetInt("TotalLevelsCleared", 0);
        PlayerPrefs.SetInt("TotalFailedAttempts", 0);
        PlayerPrefs.SetInt("SuccessfulAttempts", 0);

        UpdateStatsText();
    }
}
