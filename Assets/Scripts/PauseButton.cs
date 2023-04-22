using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    BallManager ballManager;
    public GameObject ball;

    public int mouseOnCount = 0;

    void Start()
    {
        ballManager = ball.GetComponent<BallManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ballManager.onUIElement = true;
        mouseOnCount = mouseOnCount + 1;
        Debug.Log(ballManager.onUIElement);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ballManager.onUIElement = false;
        Debug.Log(ballManager.onUIElement);
    }
}