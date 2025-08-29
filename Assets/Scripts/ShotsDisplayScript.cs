using UnityEngine;
using System.Collections.Generic;

public class ShotsDisplayScript : MonoBehaviour
{
    [Header("Shots Prefab")]
    public GameObject Ball;
    int shotCount = 2;
    List<GameObject> Balls = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < shotCount; i++)
        {
            Balls.Add(Instantiate(Ball));
            Balls[i].transform.position = new Vector3(transform.position.x + (i * 2.0f), transform.position.y, transform.position.z);

        }
    }

    void Update()
    {
        // Grey out balls based on shots_left
        if (Balls.Count == 2)
        {
            if (BallManager.shots_left == 1)
            {
                // Grey out second ball
                var sr1 = Balls[1].GetComponent<SpriteRenderer>();
                if (sr1 != null)
                    sr1.color = Color.grey;
            }
            else if (BallManager.shots_left == 0)
            {
                // Grey out both balls
                for (int i = 0; i < Balls.Count; i++)
                {
                    var sr = Balls[i].GetComponent<SpriteRenderer>();
                    if (sr != null)
                        sr.color = Color.grey;
                }
            }
        }
    }
}
