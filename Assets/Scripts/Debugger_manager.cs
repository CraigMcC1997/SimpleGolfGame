using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Level/" + Keyboard.current.anyKey.ReadValue());
            if (buildIndex != -1)
            {
                SceneManager.LoadScene(buildIndex);
            }
        }

        // Reload current level
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Move ball to mouses position when M is pressed
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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