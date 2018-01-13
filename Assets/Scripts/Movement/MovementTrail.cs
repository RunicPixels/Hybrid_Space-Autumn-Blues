using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MovementTrail : MonoBehaviour
{

    public int sections, faultMargin;
    public float sectionWidth, directionParticlesSpeed;
    public GameObject trailSectionPrefab;
<<<<<<< HEAD
    public DemoTree tree;
    public MovementTrail otherTrail;

    [HideInInspector()]
    public int currentTrailNumber = 1, currentTrailSection = 0;
=======
    public Tree tree;
>>>>>>> 5ac191d988d3e8d9b0c0ad4256bf790fe95f836e

    private TrailSection[] trailSections;
    private Vector2[] points;
    private int currentTrailNumber = 1;
    private Transform directionParticles;
    private float particlesTimer = 3;
    private bool movingParticles = false;

    // Use this for initialization
    void Awake()
    {
        CalculateTrail();
    }

    public void CalculateTrail()
    {
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach(Object obj in objects)
            if(obj.name == trailSectionPrefab.name + "(Clone)")
                    DestroyImmediate(obj);

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

        currentTrailSection = 1;
    }

    public Vector3 NextTrailSection(Vector3 currentPosition)
    {
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
        /*TrailSection trailSection = part.GetComponent<TrailSection>();
        trailSection.position1 = points[k - 1];
        trailSection.position2 = points[k];
        trailSection.sectionNumber = k;
        trailSection.width = sectionWidth;

        float sectionFactor = ((float)k / (float)sections);
        sectionFactor = Mathf.Max(sectionFactor, 0.1f);
        sectionFactor = Mathf.Min(sectionFactor, 0.8f);
        trailSection.particleLifetime = sectionFactor * 30;*/

        //trailSections[k - 1] = trailSection;

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
<<<<<<< HEAD
        return;
        if (treeGrowthTimer <= 0)
=======
        /*
        if (particlesTimer <= 0 && !movingParticles)
>>>>>>> 5ac191d988d3e8d9b0c0ad4256bf790fe95f836e
        {
            movingParticles = true;

            StartCoroutine(MoveParticles());
        }
        else
            particlesTimer -= Time.deltaTime;*/
    }

    private IEnumerator MoveParticles()
    {
        directionParticles.gameObject.SetActive(true);

        for (int i = 0; i < points.Length; i++)
        {
            directionParticles.transform.localPosition = points[i];
            yield return new WaitForSeconds(directionParticlesSpeed / sections);
        }

        directionParticles.gameObject.SetActive(false);
        particlesTimer = 3;
        movingParticles = false;
    }

    /* Check if correct section is entered */
    public bool ActivateTrailPart(int number)
    {
        return false;
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
<<<<<<< HEAD
        return;
        if (trailSections[trailSections.Length - faultMargin].highlighted)
=======
        if (trailSections[trailSections.Length - 1].highlighted)
>>>>>>> 5ac191d988d3e8d9b0c0ad4256bf790fe95f836e
        {
            tree.Grow();
            Reset();
        }
    }

    /* Reset entire trail */
    public void Reset()
    {
<<<<<<< HEAD
        yield return null;
        yield return new WaitForSeconds(waitTime);
=======
>>>>>>> 5ac191d988d3e8d9b0c0ad4256bf790fe95f836e
        foreach (TrailSection section in trailSections)
            section.Reset();
        currentTrailNumber = 1;
    }
}
