using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    public float power = 10f;

    public Vector2 minPower;
    public Vector2 maxPower;

    Camera cam;
    LineTragetory LineTrag;
    BallManager ballManager;
    Rigidbody2D Ball_rb;

    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    public bool slowedDown = true;
    public bool hasStopped = false;
    public int shots_left = 2;

    private void Start()
    {
        cam = Camera.main;
        LineTrag = GetComponent<LineTragetory>();
        ballManager = GetComponent<BallManager>();
        Ball_rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ballManager.allowControl == true)
        {
            if (Ball_rb.velocity.x <= 1 && Ball_rb.velocity.y <= 1 && Ball_rb.velocity.y >= 0)
            {
                slowedDown = true;
            }
            else
            {
                slowedDown = false;
            }

            if (Ball_rb.velocity == new Vector2(0, 0))
            {
                hasStopped = true;
            }
            else
            {
                hasStopped = false;
            }

            //starting point for mouse
            if (Input.GetMouseButtonDown(0) && slowedDown == true)
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }

            if (Input.GetMouseButton(0) && slowedDown == true)
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;
                LineTrag.RenderLine(startPoint, currentPoint);
            }

            //ending point of mouse
            if (Input.GetMouseButtonUp(0) && slowedDown == true)
            {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                //calculate force vector
                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                //apply force to ball
                Ball_rb.AddForce(force * power, ForceMode2D.Impulse);

                //stop drawing the line
                LineTrag.EndLine();

                shots_left--;
            }
        }
    }
}
