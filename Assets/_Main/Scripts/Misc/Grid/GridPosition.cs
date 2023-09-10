using System;
/// <summary>
/// A struct that represents a position in the world of a GridObject inside a LevelGrid.
/// </summary>
public struct GridPosition : IEquatable<GridPosition>
{
    public int X;
    public int Z;

    /// <summary>
    /// Constructor of the GridPosition struct.
    /// </summary>
    /// <param name="x">The x coordinate on the LevelGrid.</param>
    /// <param name="z">The z coordinate on the LevelGrid.</param>
    public GridPosition(int x, int z)
    {
        X = x;
        Z = z;
    }

    public override string ToString()
    {
        return $"x:{X};z:{Z}";
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
               X == position.X &&
               Z == position.Z;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Z);
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public static bool operator !=(GridPosition left, GridPosition right)
    {
        return !(left == right);
    }

    public static bool operator ==(GridPosition left, GridPosition right)
    {
        return left.X == right.X && left.Z == right.Z;
    }

    public static GridPosition operator +(GridPosition left, GridPosition right)
    {
        return new GridPosition(left.X + right.X, left.Z + right.Z);
    }

    public static GridPosition operator -(GridPosition left, GridPosition right)
    {
        return new GridPosition(left.X - right.X, left.Z - right.Z);
    }
}
