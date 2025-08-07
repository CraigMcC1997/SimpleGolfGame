using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // This script is attached to the pause UI element.
    // It uses Unity's EventSystem to detect pointer enter and exit events.
    // When the pointer enters the UI element, the game knows not to try to draw the drag line.
    // When the pointer exits the UI element, the game knows not it can try to draw the drag line again.

    public void OnPointerEnter(PointerEventData eventData)
    {
        BallManager.onUIElement = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        BallManager.onUIElement = false;
    }
}
