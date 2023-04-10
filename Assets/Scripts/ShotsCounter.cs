using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotsCounter : MonoBehaviour
{
    public TextMeshProUGUI shotsText;
    public BallManager bm;

    void Update()
    {
        //for shots count
        shotsText.text = "Shots Left: " + bm.dragScript.shots_left.ToString();
    }
}
