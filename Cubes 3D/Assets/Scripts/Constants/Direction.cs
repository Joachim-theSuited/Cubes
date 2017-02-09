using UnityEngine;

/// <summary>
/// Entities of this class denote specific diretions on a 2-dimensional plane.
/// The 9 predefined represent directions to the the eight primary directions on a compass, one direction to indicate no specific direction (STATIONARY) and the UNDEFINED direction, when the notion of direction is not useful or calculations where flawed.
/// A Direction has an ordinal value. This is used to work with the Animator system of Unity, because only primitives are supported.
/// </summary>
public class Direction {
    public static readonly Direction STATIONARY = new Direction(0);

    public static readonly Direction NORTH = new Direction(1);
    public static readonly Direction NORTHEAST = new Direction(2);
    public static readonly Direction EAST = new Direction(3);
    public static readonly Direction SOUTHEAST = new Direction(4);
    public static readonly Direction SOUTH = new Direction(5);
    public static readonly Direction SOUTHWEST = new Direction(6);
    public static readonly Direction WEST = new Direction(7);
    public static readonly Direction NORTHWEST = new Direction(8);

    public static readonly Direction UNDEFINED = new Direction(-1);

    public readonly int ordinal;

    private Direction(int ordinal) {
        this.ordinal = ordinal;
    }

    /// <summary>
    /// This function can be used to get the predefined Direction entity used to describe the direction given by the vector facing.
    /// Because we only have 9 directions but infinite vectors you can be facing along, this function returns the Direction closest to this vector.
    /// E.g.: If you input (0, .8) this function will return NORTH.
    /// </summary>
    /// <param name="facing">a normalized vector</param>
    /// <returns></returns>
    public static Direction FacingToDirection(Vector2 facing) {
        float deviation = .5f;

        if(MathUtilities.Similar(facing.x, 0, deviation) && MathUtilities.Similar(facing.y, 0, deviation))
            return STATIONARY;

        if(MathUtilities.Similar(facing.x, 0, deviation) && MathUtilities.Similar(facing.y, 1, deviation))
            return NORTH;

        if(MathUtilities.Similar(facing.x, 1, deviation) && MathUtilities.Similar(facing.y, 1, deviation))
            return NORTHEAST;

        if(MathUtilities.Similar(facing.x, 1, deviation) && MathUtilities.Similar(facing.y, 0, deviation))
            return EAST;

        if(MathUtilities.Similar(facing.x, 1, deviation) && MathUtilities.Similar(facing.y, -1, deviation))
            return SOUTHEAST;

        if(MathUtilities.Similar(facing.x, 0, deviation) && MathUtilities.Similar(facing.y, -1, deviation))
            return SOUTH;

        if(MathUtilities.Similar(facing.x, -1, deviation) && MathUtilities.Similar(facing.y, -1, deviation))
            return SOUTHWEST;

        if(MathUtilities.Similar(facing.x, -1, deviation) && MathUtilities.Similar(facing.y, 0, deviation))
            return WEST;

        if(MathUtilities.Similar(facing.x, -1, deviation) && MathUtilities.Similar(facing.y, 1, deviation))
            return NORTHWEST;

        return UNDEFINED;
    }

    public static Vector2 DirectionToFacing(Direction dir) {
        if(dir == NORTH)
            return Vector2.up;
        if(dir == NORTHEAST)
            return new Vector2(1, 1).normalized;
        if(dir == EAST)
            return Vector2.right;
        if(dir == SOUTHEAST)
            return new Vector2(1, -1).normalized;
        if(dir == SOUTH)
            return Vector2.down;
        if(dir == SOUTHWEST)
            return new Vector2(-1, -1).normalized;
        if(dir == WEST)
            return Vector2.left;
        if(dir == NORTHWEST)
            return new Vector2(-1, 1).normalized;

        return Vector2.zero;
    }
}
