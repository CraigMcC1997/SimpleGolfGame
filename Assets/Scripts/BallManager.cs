using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallManager : MonoBehaviour
{
    Vector3 originalPos;
    Vector3 originalVelocity;

    const int MAX_TURNS = 2;
    public bool allowControl = true;

    public Rigidbody2D rb;
    DragAndShoot dragScript;
    Renderer renderer;

    bool quit = false;
    float counter = 0;
    bool switchCounters = false;
    const int MAX_TIMER = 500; //TODO: make this more robust, random number is not good

    void Start()
    {
        dragScript = GetComponent<DragAndShoot>();

        originalPos = gameObject.transform.position;
        originalVelocity = rb.velocity;
        renderer = GetComponent<Renderer>();
    }

    IEnumerator waiter(float waitTime)
    {
        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            Debug.Log("We have waited for: " + counter + " seconds");
            //Wait for a frame so that Unity doesn't freeze
            //Check if we want to quit this function
            if (quit)
            {
                //Quit function
                yield break;
            }
            yield return null;
        }
    }

    void Update()
    {
        // stop players controll and show death screen
        if (dragScript.turns_taken == MAX_TURNS)
        {
            allowControl = false;

            StartCoroutine(waiter(MAX_TIMER));

            if (counter >= MAX_TIMER)
            {
                if (dragScript.hasStopped == true)
                {
                    SceneManager.LoadScene("Game Over");
                }
            }
        }

        // keep ball on screen
        if (!renderer.isVisible)
        {
            rb.transform.position = originalPos;
            rb.velocity = originalVelocity;
        }
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