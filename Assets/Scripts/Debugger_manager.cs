using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger_manager : MonoBehaviour
{

    public static Debugger_manager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        } 
        
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        // load the level corresponding to the number key pressed (1 = level 1, etc)
        if (Input.anyKeyDown)
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Level/" + Input.inputString);
            if (buildIndex != -1)
            {
                SceneManager.LoadScene(buildIndex);
            }
        }

        // Reload current level
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Move ball to mouses position when M is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; // Ensure z-coordinate is zero for 2D
            GameObject ball = GameObject.FindGameObjectWithTag("Player");
            if (ball != null)
            {
                ball.transform.position = mousePos;
                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                if (ballRb != null)
                {
                    ballRb.linearVelocity = Vector2.zero; // Stop ball movement   
                }
            }
        }
    }
}