using UnityEngine;

public class TragectoryScript : MonoBehaviour
{
    public GameObject ballObject;
    Camera cam;
    Vector2 Direction;
    public GameObject pointsPrefab;
    public GameObject[] points;
    int pointsIndex = 15;

    [SerializeField] float power = 3.0f;
    [SerializeField] Vector2 minPower;
    [SerializeField] Vector2 maxPower;

    Vector2 force;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = ballObject.transform.position;
        cam = Camera.main;
        points = new GameObject[pointsIndex];

        for (int i = 0; i < pointsIndex; i++)
        {
            points[i] = Instantiate(pointsPrefab, transform.position, Quaternion.identity);
            points[i].SetActive(false);
        }
    }

    Vector2 pointPos(float t)
    {
        return (Vector2)transform.position + (Direction.normalized * force * t) + 0.05f * Physics2D.gravity * (t * t);
    }

    void faceMouse()
    {
        transform.right = Direction;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clubPos = transform.position;

        Direction = mousePos - clubPos;

        force = new Vector2(Mathf.Clamp(mousePos.x - clubPos.x, minPower.x, maxPower.x),
                    Mathf.Clamp(mousePos.y - clubPos.y, minPower.y, maxPower.y));

        Debug.Log("force: " + force);

        // not required as object is a ball, but left for testing purposes
        faceMouse();

        for (int i = 0; i < pointsIndex; i++)
        {
            points[i].transform.position = pointPos(i * 0.1f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < pointsIndex; i++)
            {
                points[i].SetActive(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ballObject.GetComponent<Rigidbody2D>().linearVelocity = force * power;
            BallManager.shots_left--;

            for (int i = 0; i < pointsIndex; i++)
            {
                points[i].SetActive(false);
            }
        }
    }
}
