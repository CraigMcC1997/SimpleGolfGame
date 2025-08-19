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
            Balls[i].transform.position = new Vector3(transform.position.x + (i * 5), transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        
    }
}
