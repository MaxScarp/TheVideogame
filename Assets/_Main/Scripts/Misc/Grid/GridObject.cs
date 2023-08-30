using System.Collections.Generic;

/// <summary>
/// Class that represents a square on a specified LevelGrid.
/// </summary>
public class GridObject
{
    private LevelGrid levelGrid;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    /// <summary>
    /// Constructor of the GridObject class.
    /// </summary>
    /// <param name="levelGrid">The LevelGrid to which this GridObject belongs to.</param>
    /// <param name="gridPosition">The GridPosition of this GridObject.</param>
    public GridObject(LevelGrid levelGrid, GridPosition gridPosition)
    {
        this.levelGrid = levelGrid;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += $"{unit}\n";
        }

        return $"{gridPosition}\n{unitString}";
    }

    /// <summary>
    /// Add the specified Unit to this GridObject.
    /// </summary>
    /// <param name="unit">The specified Unit.</param>
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    /// <summary>
    /// Remove the specified Unit to this GridObject.
    /// </summary>
    /// <param name="unit">The specified Unit.</param>
    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    /// <summary>
    /// Check if there is any unit on this GridObject.
    /// </summary>
    /// <returns>True if there is at least on unit, otherwise False.</returns>
    public bool HasAnyUnit() => unitList.Count > 0;

    /// <summary>
    /// Get the unitList.
    /// </summary>
    /// <returns>A list containing all the unit inside this GridObject.</returns>
    public List<Unit> GetUnitList() => unitList;
}
