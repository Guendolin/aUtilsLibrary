using UnityEngine;

public static class Vector3Extensions
{
    /// <summary>
    /// Return true if c is between a and b.
    /// </summary>
    /// Betweeen is defined by being between two planes with the normal ab that is positioned at a and b.
    public static bool IsBetween(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 ab = b - a;
        Vector3 ac = c - a;
        float dot = Vector3.Dot(ab.normalized, ac);
        return dot > 0f && dot < ab.magnitude;
    }

    public static Vector3 FindClosestVector(this Vector3 target, params Vector3[] vectors)
    {
        float angle;
        return FindClosestVector(target, out angle, vectors);
    }

    public static Vector3 FindClosestVector(this Vector3 target, out float angle, params Vector3[] vectors)
    {
        angle = -1f;
        if (vectors.Length == 1)
        {
            return vectors[0];
        }

        float smallestAngle;
        float currentAngle;

        Vector3 closestAxis = vectors[0];
        smallestAngle = Vector3.Angle(target, vectors[0]);

        for (int i = 1; i < vectors.Length; i++)
        {
            Vector3 axis = vectors[i];
            currentAngle = Vector3.Angle(target, axis);
            if (currentAngle < smallestAngle)
            {
                closestAxis = axis;
                smallestAngle = currentAngle;
            }
        }

        angle = smallestAngle;
        return closestAxis;
    }
}
