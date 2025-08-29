using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger_manager : MonoBehaviour
{
    void Start()
    {
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
