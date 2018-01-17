using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class MovementTrail : MonoBehaviour
{
    public bool repetitionTrail = false;
    public int sections;
    public bool paused = false, reverse = false;
    public GameObject trailSectionPrefab, target;

    [HideInInspector()]
    public int currentTrailSection = 0;
    [HideInInspector()]
    public bool targetActive = false;

    private float particleLifetime = 1;
    private int repetitions = 0;
    private bool isReversing = false;
    private TrailSection[] trailSections;
    private Vector2[] points;
    private Vector3 targetStartPosition;
    private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    // Use this for initialization
    protected virtual void Start()
    {
        CalculateTrail();

        target.transform.position = points[0];

        targetStartPosition = target.transform.position;
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
            
            Vector2 point = CalculatePoint(t, p);

            points[k] = point;

            if (k == 0)
                continue;

            InstantiateSection(k);
        }

        currentTrailSection = 0;
    }

    public Vector3 NextTrailSection(Vector3 currentPosition)
    {
        if(paused)
            return points[currentTrailSection];

        if (Vector3.Distance(points[currentTrailSection], currentPosition) <= 0.01f)
        {
            if(isReversing)
            {
                currentTrailSection--;
                if (currentTrailSection == -1)
                {
                    currentTrailSection = 0;
                    isReversing = false;
                }
            }
            else
            {
                currentTrailSection++;
                if (currentTrailSection == points.Length)
                {
                    currentTrailSection = points.Length - 1;
                    EndRepetition();
                    if (reverse)
                        isReversing = true;

                }
            }

            return points[currentTrailSection];
        }
        else
            return points[currentTrailSection];
    }

    private void EndRepetition()
    {
        if(!reverse)
            currentTrailSection = 0;
        if (repetitionTrail)
        {
            repetitions++;
            if (repetitions == 3)
            {
                MovementManager.instance.NextMovement();
            }
        }
    }

    private void InstantiateSection(int k)
    {
        GameObject part = (GameObject)Instantiate(trailSectionPrefab, points[k], Quaternion.identity);
        part.transform.parent = transform;
        particleSystems.Add(part.transform.GetChild(0).GetComponent<ParticleSystem>());
    }

    public void DisableParticles()
    {
        foreach(ParticleSystem ps in particleSystems)
        {
            var main = ps.main;
            main.startLifetime = 0;
        }
    }

    public void EnableParticles()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            var main = ps.main;
            main.startLifetime = particleLifetime;
        }
    }

    public void ResetTrail()
    {
        currentTrailSection = 0;
        repetitions = 0;
        target.transform.position = targetStartPosition;
        paused = true;
    }

    /* Recursive method implementing de Casteljau's algorithm to calculate a single point on a curve described by n points */
    protected virtual Vector2 CalculatePoint(float t, Vector2[] points)
    {
        Vector2[] newPoints = new Vector2[points.Length - 1];

        for (int i = 1; i < points.Length; i++)
            newPoints[i - 1] = CalculatePointOnLine(t, points[i - 1], points[i]);

        if (newPoints.Length > 1)
            return CalculatePoint(t, newPoints);
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

    protected virtual void Update()
    {
    }
}
