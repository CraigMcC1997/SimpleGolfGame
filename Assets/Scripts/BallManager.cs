using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class BallManager : MonoBehaviour
{
    public GameManager gameManager;
    Vector3 startingPos;
    Vector3 originalVelocity;

    public const int MAX_SHOTS = 2;
    public static int shots_left = 2;

    public static bool allowControl = true;
    public static bool onUIElement = false;

    Rigidbody2D Ball_rb;

    void Start()
    {
        // Reset all static state for new game
        shots_left = MAX_SHOTS; // Initialize shots left
        allowControl = true;
        onUIElement = false;

        Ball_rb = GetComponent<Rigidbody2D>();
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
            gameObject.GetComponent<TrailRenderer>().Clear();
        }
    }

    void preventPlayerControl()
    {
        float stopThreshold = 0.05f;
        float velocity = Ball_rb.linearVelocity.magnitude;

        // If ball is moving, prevent control
        if (velocity > stopThreshold || shots_left <= 0)
        {
            allowControl = false;
        }
        // ball has slowed down to a crawl
        else
        {
            allowControl = true;
        }
    }

    void Update()
    {
        preventPlayerControl();
        KeepBallOnScreen();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "End Flag")
        {
            gameManager.LoadNextLevel();
        }
    }

    public float getVelocity()
    {
        return Ball_rb.linearVelocity.magnitude;
    }
}
