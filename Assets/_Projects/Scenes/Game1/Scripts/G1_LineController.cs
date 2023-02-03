using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class G1_LineController : MonoBehaviour
{
    private LineRenderer lr;
    private List<Transform> points = new List<Transform>();

    private void Awake()
    {
        lr= GetComponent<LineRenderer>();
        ClearLines();
    }

    public void SetUpLine(Transform _points)
    {
        points.Add(_points);

        lr.positionCount = points.Count;
    }

    public void ClearLines()
    {
        points.Clear();
        lr.positionCount = 0;
    }

    private void Update()
    {
        for (int i=0; i<points.Count; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
