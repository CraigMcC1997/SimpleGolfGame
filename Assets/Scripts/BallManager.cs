using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BallManager : MonoBehaviour
{
    public GameObject gameManager;
    GameManager gameManagerScript;
    Vector3 startingPos;
    Vector3 originalVelocity;

    public static int shots_left = 2;

    const int MAX_TURNS = 2;
    public static bool allowControl = true;
    public static bool onUIElement = false;
    public static bool stopped = false;

    Rigidbody2D Ball_rb;

    void Start()
    {
        shots_left = MAX_TURNS; // Initialize shots left
        Ball_rb = GetComponent<Rigidbody2D>();

        gameManagerScript = gameManager.GetComponent<GameManager>();

        startingPos = gameObject.transform.position;
        originalVelocity = Ball_rb.linearVelocity;
    }

    void KeepBallOnScreen()
    {
        Vector2 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);

        if (playerPosScreen.x > Screen.width || playerPosScreen.x < 0.0f)
        {
            Ball_rb.transform.position = startingPos;
            Ball_rb.linearVelocity = originalVelocity;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            allowControl = false;
        }
        else
        {
            allowControl = true;
        }

        if (Ball_rb.linearVelocity.magnitude > 0.0f)
        {
            stopped = false;
        }
        else
        {
            stopped = true;
        }

       // Debug.Log("Ball stopped: " + stopped);

        // keep ball on screen
        KeepBallOnScreen();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "End Flag")
        {
            gameManagerScript.LoadNextLevel();
        }
    }
}