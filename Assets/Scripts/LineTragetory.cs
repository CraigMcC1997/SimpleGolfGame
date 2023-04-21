using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTragetory : MonoBehaviour
{
    LineRenderer lr;

    // Awake is a Unity callback function that is called when the script component is first initialized.
    // It retrieves the LineRenderer component attached to the same game object as this script.
    // Note: This function is marked as private, meaning it can only be accessed within the same class.
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // RenderLine is a public function that sets the positions of a LineRenderer to draw a line between two points in 3D space.
    // It takes in two Vector3 parameters: start (the starting point of the line) and end (the ending point of the line).
    // It sets the position count of the LineRenderer to 2, creates an array of Vector3 points with the start and end points,
    // and sets these points as the positions of the LineRenderer to draw the line.
    public void RenderLine(Vector3 start, Vector3 end)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = start;
        points[1] = end;

        lr.SetPositions(points);
    }

    // EndLine is a public function that clears the positions of the LineRenderer, effectively ending the drawn line.
    // It sets the position count of the LineRenderer to 0, effectively clearing any drawn line.
    public void EndLine()
    {
        lr.positionCount = 0;
    }
}
