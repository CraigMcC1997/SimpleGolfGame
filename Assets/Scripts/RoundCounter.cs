using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundCounter : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    // Start is a Unity callback function that is called when the script component is enabled and starts running.
    // It sets the round text to display the current round based on the active scene's name.
    void Start()
    {
        roundText.text = "Round " + SceneManager.GetActiveScene().name;
    }
}
