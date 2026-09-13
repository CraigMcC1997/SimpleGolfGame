using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUp;
    public TextMeshProUGUI tutorialText;

    //public AudioClip clickAUDIO;
    //AudioSource audioSource;
    int shieldButtonClicked = 0;
    const int MAX_TUTORIAL_STEPS = 5;

    void Start()
    {
        popUp.SetActive(true);
        //audioSource = GetComponent<AudioSource>();

        BallManager.allowControl = false;
        SetTutorialText(shieldButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetTutorialText(int step)
    {
        switch (step)
        {
            case 0:
                // Initial step text
                tutorialText.text = "Welcome to the tutorial!";
                break;
            case 1:
                // Step 1 text
                tutorialText.text = "Fire the ball by clicking anywhere on the screen and dragging back to increase the shots power, release to shoot!";
                break;
            case 2:
                // Step 2 text
                tutorialText.text = "Aim for the hole with the flag to complete the level!";
                break;
            case 3:
                // Step 3 text
                tutorialText.text = "You only have 2 shots per level. MAKE THEM COUNT!";
                break;
            case 4:
                // Step 4 text
                tutorialText.text = "Top left of your screen displays your current level. Reach the final level without failing to finish! Otherwise you'll have to start over from the beginning!";
                break;
            case 5:
                // Step 5 text
                tutorialText.text = "Good luck and have fun!";
                break;
            default:
                break;
        }
    }

    public void ContinueTutorial()
    {
        shieldButtonClicked++;

        if (shieldButtonClicked > MAX_TUTORIAL_STEPS)
        {
            ClosePopUp();
            return;
        }
        SetTutorialText(shieldButtonClicked);
    }

    public void ClosePopUp()
    {
        //audioSource.PlayOneShot(clickAUDIO);
        popUp.SetActive(false);
        BallManager.allowControl = true;
        PlayerPrefs.SetInt("TutorialCompleted", 1);
    }
}
