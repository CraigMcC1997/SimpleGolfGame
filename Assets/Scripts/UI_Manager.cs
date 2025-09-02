using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text roundText;

    void Start()
    {
        // Initialize the round text when the game starts
        UpdateRoundText();
    }

    public void UpdateRoundText()
    {
        // Update the round text based on the current scene name
        roundText.text = SceneManager.GetActiveScene().name;
    }
}
