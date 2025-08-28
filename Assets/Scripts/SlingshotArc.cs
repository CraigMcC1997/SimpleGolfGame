using UnityEngine;
using System.Collections.Generic;

public class SlingshotArc : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject Ball;
    private Rigidbody2D rb;

    [Header("Arc Settings")]
    public GameObject dotPrefab;
    public GameObject startingPointMarker;
    public int dotCount = 20;
    public float timeStep = 0.005f;
    public float maxDistance = 5f;         // Max pullback distance
    public float minDragThreshold = 0.3f; // minimum drag distance to start drawing
    bool dragValid = false;

    [Header("Throw Settings")]
    public float throwForceMultiplier = 10f;

    Camera mainCamera;
    bool isDragging;
    Vector3 dragStartPos;
    List<GameObject> dots = new List<GameObject>();
    GameObject startingPointMarkerInstance;

    float gravity;

    Vector2 launchVelocity;

    void Start()
    {
        mainCamera = Camera.main;
        rb = Ball.GetComponent<Rigidbody2D>();

        // Get gravity value from Unity physics
        gravity = Physics2D.gravity.y * rb.gravityScale;

        // Create dot pool so we don’t instantiate every frame
        for (int i = 0; i < dotCount; i++)
        {
            GameObject dot = Instantiate(dotPrefab, Ball.transform.position, Quaternion.identity, transform);
            dot.SetActive(false);
            dots.Add(dot);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && BallManager.allowControl)
        {
            isDragging = true;
            dragValid = false;
            dragStartPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            dragStartPos.z = 0;

            // create the starting marker
            startingPointMarkerInstance = Instantiate(startingPointMarker, dragStartPos, Quaternion.identity, transform);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging && dragValid)
            {
                BallManager.allowControl = false;
                ThrowObject();
            }

            //kill the marker
            Destroy(startingPointMarkerInstance);

            isDragging = false;
            dragValid = false;
            HideDots();
        }

        if (isDragging)
        {
            Vector3 currentMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            currentMousePos.z = 0;

            float dragDistance = Vector3.Distance(dragStartPos, currentMousePos);
            //Debug.Log($"Drag Distance: {dragDistance}");
            if (dragDistance > minDragThreshold)
            {
                dragValid = true;
                ShowArc();
            }
            else
            {
                dragValid = false;
                HideDots(); // Hide while drag is too small
            }
        }
    }

    void calculateLaunchVelocity()
    {
        Vector3 currentMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentMousePos.z = 0;

        float dragDistance = Vector3.Distance(dragStartPos, currentMousePos);
        if (dragDistance < 0.001f) return;

        float clampedDistance = Mathf.Min(dragDistance, maxDistance);

        Vector3 dragVector = currentMousePos - dragStartPos;
        if (dragVector.sqrMagnitude < 0.000001f) return;
        Vector3 throwDirection = -dragVector.normalized;

        launchVelocity = throwDirection * clampedDistance * throwForceMultiplier;
    }

    void ShowArc()
    {
        calculateLaunchVelocity();

        for (int i = 0; i < dotCount; i++)
        {
            float t = i * timeStep;
            Vector2 position = (Vector2)Ball.transform.position + launchVelocity * t +
                               0.5f * new Vector2(0, gravity) * t * t;

            if (IsVectorValid(position))
            {
                dots[i].transform.position = position;
                dots[i].SetActive(true);

                // dots fade out over distance
                float alpha = 1f - (i / (float)(dotCount - 1));
                var sr = dots[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = alpha;
                    sr.color = c;
                }
            }
            else
            {
                dots[i].SetActive(false);
            }
        }
    }

    void ThrowObject()
    {
        BallManager.shots_left--;
        rb.linearVelocity = launchVelocity;
    }

    void HideDots()
    {
        foreach (var dot in dots)
        {
            dot.SetActive(false);
        }
    }

    bool IsVectorValid(Vector2 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) ||
                 float.IsInfinity(v.x) || float.IsInfinity(v.y));
    }
}
