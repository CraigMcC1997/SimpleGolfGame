using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    LineTragetory lt;
    BallManager ballManager;

    Camera cam;

    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public bool isStill = true;
    public bool hasStopped = false;
    public int shots_left = 2;

    private void Start()
    {
        cam = Camera.main;
        lt = GetComponent<LineTragetory>();
        ballManager = GetComponent<BallManager>();
    }

    private void Update()
    {
        if (ballManager.allowControl == true)
        {
            if (rb.velocity.x <= 1 && rb.velocity.y <= 1 && rb.velocity.y >= 0)
            {
                isStill = true;
            }
            else
            {
                isStill = false;
            }

            if (rb.velocity == new Vector2(0, 0))
            {
                hasStopped = true;
            }
            else
            {
                hasStopped = false;
            }

            //starting point for mouse
            if (Input.GetMouseButtonDown(0) && isStill == true)
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }

            if (Input.GetMouseButton(0) && isStill == true)
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;
                lt.RenderLine(startPoint, currentPoint);
            }

            //ending point of mouse
            if (Input.GetMouseButtonUp(0) && isStill == true)
            {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                //calculate force vector
                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                //apply force to ball
                rb.AddForce(force * power, ForceMode2D.Impulse);

                //stop drawing the line
                lt.EndLine();

                shots_left--;
            }

            // Debug.Log(shots_left);
        }
    }
}
