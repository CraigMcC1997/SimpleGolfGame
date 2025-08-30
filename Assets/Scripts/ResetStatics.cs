using UnityEngine;

public class ResetStatics : MonoBehaviour
{
    /* reset all statics when loading in the menu to configure the game state back to default*/
    void Start()
    {
        /* allow the player to use a 2nd chance when they fail a level */
        GameManager.allowSecondChance = true;
    }
    
}
