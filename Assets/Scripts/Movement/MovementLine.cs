using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLine : MovementTrail {

    protected override Vector2 CalculatePoint(float t, Vector2[] points)
    {
        return points[0] + (points[1] - points[0]) * t;
    }
}
