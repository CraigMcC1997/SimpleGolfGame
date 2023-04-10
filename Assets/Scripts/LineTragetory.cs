using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTragetory : MonoBehaviour
{
    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void RenderLine(Vector3 start, Vector3 end)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = start;
        points[1] = end;

        lr.SetPositions(points);
    }

    public void EndLine()
    {
        lr.positionCount = 0;
    }
}
