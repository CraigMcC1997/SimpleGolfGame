using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugger_manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
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
