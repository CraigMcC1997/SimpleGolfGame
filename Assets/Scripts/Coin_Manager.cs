using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Coin_Manager : MonoBehaviour
{
    public void UpdateCoinCount(int coins = 1)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        totalCoins += coins;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
    }

    public int GetCoinCount()
    {
        return PlayerPrefs.GetInt("TotalCoins", 0);
    }
}
