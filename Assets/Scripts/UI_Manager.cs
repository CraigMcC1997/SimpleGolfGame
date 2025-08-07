using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TMP_Text roundText;
    public TMP_Text shotsText;

    void Start()
    {
        // Initialize the round text when the game starts
        UpdateRoundText();
    }

    void Update()
    {
        // Continuously update the shots text
        UpdateShotsText();
    }

    public void UpdateShotsText()
    {
        // Update the shots count text
        shotsText.text = "Shots Left: " + DragAndShoot.shots_left.ToString();
    }

    public void UpdateRoundText()
    {
        // Update the round text based on the current scene name
        roundText.text = SceneManager.GetActiveScene().name;
    }
}
