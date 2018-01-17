using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class MovementTrail : MonoBehaviour
{

    public int sections;
    public bool paused = true;
    public GameObject trailSectionPrefab;

    [HideInInspector()]
    public int currentTrailNumber = 1, currentTrailSection = 0;

    private TrailSection[] trailSections;
    private Vector2[] points;

    // Use this for initialization
    void Start()
    {
        CalculateTrail();
    }

    public void CalculateTrail()
    {
        // Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        // foreach(Object obj in objects)
        //     if(obj.name == trailSectionPrefab.name + "(Clone)")
        //             DestroyImmediate(obj);

        points = new Vector2[sections + 1];
        trailSections = new TrailSection[sections];

        Vector2[] p = new Vector2[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            p[i] = transform.GetChild(i).position;
        
        for (int k = 0; k <= sections; k++)
        {
            float t = (float)k / sections;

            Vector2 point = CalculatePointOnCurve(t, p);

            points[k] = point;

            if (k == 0)
                continue;

            InstantiateSection(k);
        }

        currentTrailSection = points.Length - 1;
    }

    public Vector3 NextTrailSection(Vector3 currentPosition)
    {
        if(paused)
            return points[currentTrailSection];

        if (Vector3.Distance(points[currentTrailSection], currentPosition) <= 0.01f)
        {
            currentTrailSection++;
            if(currentTrailSection == points.Length)
                currentTrailSection = 0;
            return points[currentTrailSection];
        }
        else
            return points[currentTrailSection];
    }

    private void InstantiateSection(int k)
    {
        GameObject part = (GameObject)Instantiate(trailSectionPrefab, points[k], Quaternion.identity);
        part.transform.parent = transform;
    }

    /* Recursive method implementing de Casteljau's algorithm to calculate a single point on a curve described by n points */
    private Vector2 CalculatePointOnCurve(float t, Vector2[] points)
    {
        Vector2[] newPoints = new Vector2[points.Length - 1];

        for (int i = 1; i < points.Length; i++)
            newPoints[i - 1] = CalculatePointOnLine(t, points[i - 1], points[i]);

        if (newPoints.Length > 1)
            return CalculatePointOnCurve(t, newPoints);
        else
            return newPoints[0];
    }

    private Vector2 CalculatePointOnLine(float t, Vector2 pointA, Vector2 pointB)
    {
        Vector2 result;
        result.x = pointA.x - ((pointA.x - pointB.x) * t);
        result.y = pointA.y - ((pointA.y - pointB.y) * t);
        return result;
    }
    
    private void Update()
    {
    }
}
