using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BallManager : MonoBehaviour
{
    Vector3 originalPos;
    Vector3 originalVelocity;

    const int MAX_TURNS = 2;
    public bool allowControl = true;

    public Rigidbody2D rb;
    DragAndShoot dragScript;
    Renderer renderer;

    public TextMeshProUGUI shotsText;

    void Start()
    {
        dragScript = GetComponent<DragAndShoot>();

        originalPos = gameObject.transform.position;
        originalVelocity = rb.velocity;
        renderer = GetComponent<Renderer>();
    }

    //temp to allow invoke to wait x seconds before calling game over
    void loadGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    void Update()
    {
        // stop players controll and show death screen
        if (dragScript.turns_taken == MAX_TURNS)
        {
            allowControl = false;

            Invoke("loadGameOver", 5);
        }

        //for score count
        shotsText.text = "Shots Left: " + dragScript.turns_taken.ToString();

        // keep ball on screen
        //if (!renderer.isVisible)
        //{
        //    rb.transform.position = originalPos;
        //    rb.velocity = originalVelocity;
        //}
    }

    void resetPosition(Collision2D other)
    {
        if (other.gameObject.tag == "End Flag")
        {
            //load next scene assuming build order is correct
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        resetPosition(other);
    }
}