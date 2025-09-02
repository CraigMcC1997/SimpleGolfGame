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
        if (Input.anyKeyDown)
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Level/" + Input.inputString);
            if (buildIndex != -1)
            {
                SceneManager.LoadScene(buildIndex);
            }
        }
    }
}
