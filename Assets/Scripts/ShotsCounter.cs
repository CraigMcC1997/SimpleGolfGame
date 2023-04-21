using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShotsCounter : MonoBehaviour
{
    public TextMeshProUGUI shotsText;
    public BallManager bm;

    // Update is a Unity callback function that is called once per frame.
    // It updates the shots count text based on the remaining shots from the drag script.
    void Update()
    {
        shotsText.text = "Shots Left: " + bm.dragScript.shots_left.ToString();
    }
}
