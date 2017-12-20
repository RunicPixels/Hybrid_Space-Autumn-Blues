using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrail : MonoBehaviour
{

    public int sections, faultMargin;
    public float sectionWidth, treeGrowth;
    public GameObject trailSectionPrefab;
    public DemoTree tree;

    private TrailSection[] trailSections;
    private Vector2[] points;
    private int currentTrailNumber = 1;
    private Transform directionParticles;
    private float treeGrowthTimer = 0;

    // Use this for initialization
    void Start()
    {
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
    }

    private void InstantiateSection(int k)
    {
        GameObject part = (GameObject)Instantiate(trailSectionPrefab, points[k], Quaternion.identity);
        part.transform.parent = transform;
        TrailSection trailSection = part.GetComponent<TrailSection>();
        trailSection.position1 = points[k - 1];
        trailSection.position2 = points[k];
        trailSection.sectionNumber = k;
        trailSection.width = sectionWidth;

        float sectionFactor = ((float)k / (float)sections);
        sectionFactor = Mathf.Max(sectionFactor, 0.1f);
        sectionFactor = Mathf.Min(sectionFactor, 0.8f);
        trailSection.particleLifetime = sectionFactor * 30;

        trailSections[k - 1] = trailSection;

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
        if (treeGrowthTimer <= 0)
        {
            tree.growth = false;
        }
        else
        {
            tree.growth = true;
            treeGrowthTimer -= Time.deltaTime;
        }
    }

    /* Check if correct section is entered */
    public bool ActivateTrailPart(int number)
    {
        if (number <= currentTrailNumber + faultMargin)
        {
            for (int i = currentTrailNumber - 1 ; i < number; i++)
                trailSections[i].Highlight();
            currentTrailNumber = number;

            CheckCompletion();
            return true;
        }

        Reset();
        return false;
    }

    private void CheckCompletion()
    {
        if (trailSections[trailSections.Length - 1].highlighted)
        {
            treeGrowthTimer = treeGrowth;
            Reset();
        }
    }

    /* Reset entire trail */
    public void Reset()
    {
        foreach (TrailSection section in trailSections)
            section.Reset();
        currentTrailNumber = 1;
    }
}
