using UnityEngine;

public static class Vector2Extensions
{

    /// <summary>
    /// Returns true  if values is within the x (inclusive) and y (exlusive) otherwise false
    /// </summary>
    public static bool InRange(this Vector2 v, float value)
    {
        return value >= v.x && value < v.y;
    }

    /// <summary>
    /// Return a random float number between x [inclusive] and y [inclusive]
    /// </summary>
    public static float Random(this Vector2 v)
    {
        return UnityEngine.Random.Range(v.x, v.y);
    }
}
