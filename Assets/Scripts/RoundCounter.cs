using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundCounter : MonoBehaviour
{
    public TextMeshProUGUI shotsText;
    void Update()
    {
        //for round count
        shotsText.text = "Round " + SceneManager.GetActiveScene().name;
    }
}