using UnityEngine;

/// <summary>
/// This class contains mathematical functions, that are not part of the standard library or Unity but proved to be useful.
/// </summary>
public abstract class MathUtilities {

    /// <summary>
    /// This function is very similar to Mathf.Approximately but takes an additional parameter epsilon to allow to define your own.
    /// This effectively calculates |a - b| &lt;= epsilon.
    /// Calling this method may be somewhat shorter and cleaner, then writing the term itself.
    /// </summary>
    /// <param name="a">first number</param>
    /// <param name="b">second number</param>
    /// <param name="epsilon">amount they may differ</param>
    /// <returns>true iff |a - b| &lt;= epsilon</returns>
    public static bool Similar(float a, float b, float epsilon)
    {
        return Mathf.Abs(a - b) <= epsilon;
    }

    /// <summary>
    /// Returns a copy of the vector with the y-coordinate flipped.
    /// </summary>
    public static Vector2 FlipY(Vector2 v) {
        return Vector2.Scale(v, new Vector2(1, -1));
    }

}
