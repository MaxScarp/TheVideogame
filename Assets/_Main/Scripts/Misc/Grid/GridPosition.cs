/// <summary>
/// A struct that represents a position in the world of a GridObject inside a LevelGrid.
/// </summary>
public struct GridPosition
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
        return $"x:{X};\nz:{Z}";
    }
}
