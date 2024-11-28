using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StringScript : MonoBehaviour
{
    public Transform startpoint, grabpoint;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreateString(Vector3? endPosition)
    {
        Vector3[] linePoints = new Vector3[2];
        linePoints[0] = startpoint.localPosition;
        if (endPosition != null)
        {
            linePoints[1] = transform.InverseTransformPoint(endPosition.Value);
        }
        else
        {
            linePoints[1] = grabpoint.localPosition;
        }
        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateString(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
