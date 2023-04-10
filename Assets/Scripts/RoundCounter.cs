using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundCounter : MonoBehaviour
{
    public TextMeshProUGUI roundText;

    void Start()
    {
        //for round count
        roundText.text = "Round " + SceneManager.GetActiveScene().name;
    }
}
