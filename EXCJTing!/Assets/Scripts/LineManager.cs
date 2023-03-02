using System.Collections;
using System.Linq;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab;  // The prefab to use for the line
    public float lineDrawSpeed = 20f;  // The speed at which the line is drawn
    public float minDistance = 0.1f;  // The minimum distance between two points to draw a line

    private GameObject currentLine;  // The current line being drawn
    private LineRenderer lineRenderer;  // The LineRenderer component of the current line
    private EdgeCollider2D edgeCollider;  // The EdgeCollider2D component of the current line
    private Vector2 lastMousePosition;  // The last mouse position recorded
    private bool isErasing = false;  // Flag to indicate if eraser mode is active
    private int eraseSegmentIndex = -1;  // The index of the line segment to be erased

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isErasing)
            {
                CreateNewLine();
            }
            else
            {
                CheckEraseSegment();
            }
        }

        if (currentLine != null)
        {
            if (!isErasing)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Vector2.Distance(mousePosition, lastMousePosition) > minDistance)
                {
                    StartCoroutine(DrawLine(mousePosition));
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            isErasing = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isErasing = false;
            eraseSegmentIndex = -1;
        }
    }

    void CreateNewLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, lastMousePosition);
        lineRenderer.positionCount = 1;
        edgeCollider.points = new Vector2[] { lastMousePosition, lastMousePosition };
    }

    IEnumerator DrawLine(Vector2 toPosition)
    {
        float distance = Vector2.Distance(lastMousePosition, toPosition);
        float drawTime = distance / lineDrawSpeed;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / drawTime;
            Vector2 currentPosition = Vector2.Lerp(lastMousePosition, toPosition, t);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);

            Vector2[] colliderPoints = edgeCollider.points;
            colliderPoints[colliderPoints.Length - 1] = currentPosition;
            edgeCollider.points = colliderPoints;

            yield return null;
        }

        lastMousePosition = toPosition;
    }

    void CheckEraseSegment()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int segmentIndex = GetSegmentIndexAtPoint(mousePosition);

        if (segmentIndex != -1 && segmentIndex != eraseSegmentIndex)
        {
            eraseSegmentIndex = segmentIndex;
            lineRenderer.positionCount--;
            Vector2[] colliderPoints = edgeCollider.points;
            colliderPoints = colliderPoints.Where((val, index) => index != eraseSegmentIndex && index != eraseSegmentIndex + 1).ToArray();
            edgeCollider.points = colliderPoints;
        }
    }

    int GetSegmentIndexAtPoint(Vector2 point)
    {
        if (lineRenderer.positionCount < 2)
        {
            return -1;
        }

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector2 segmentStart = lineRenderer.GetPosition(i);
            Vector2 segmentEnd = lineRenderer.GetPosition(i + 1);

            if (Vector2.Distance(point, segmentStart) < minDistance)
            {
                return i;
            }
            else
            {
                float segmentLength = Vector2.Distance(segmentStart, segmentEnd);
                float distanceToSegmentStart = Vector2.Distance(point, segmentStart);
                float distanceToSegmentEnd = Vector2.Distance(point, segmentEnd);

                if (distanceToSegmentStart + distanceToSegmentEnd - segmentLength < minDistance)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    public void ClearLines()
    {
        Destroy(currentLine);
        currentLine = null;
        lineRenderer = null;
        edgeCollider = null;
    }
}