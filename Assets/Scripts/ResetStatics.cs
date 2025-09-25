using UnityEngine;

public class ResetStatics : MonoBehaviour
{
    public void ResetAllStatics()
    {
        PlayerPrefs.SetInt("TotalShots", 0);
        PlayerPrefs.SetInt("TotalLevelsCleared", 0);
        PlayerPrefs.SetInt("TotalFailedAttempts", 0);
        PlayerPrefs.SetInt("SuccessfulAttempts", 0);
    }
}
