using System.Collections.Generic;

/// <summary>
/// Class that represents a square on a specified LevelGrid.
/// </summary>
public class GridObject
{
    public int VisbilityLevel
    {
        get
        {
            return visibilityLevel;
        }
        set
        {
            visibilityLevel = value;
            UpdateVisibilityLevelType();
        }
    }

    private LevelGrid levelGrid;
    private GridPosition gridPosition;
    private List<Unit> unitList;
    private GridSystemManager.VisibilityLevelType visibilityLevelType;
    private int visibilityLevel;

    /// <summary>
    /// Constructor of the GridObject class.
    /// </summary>
    /// <param name="levelGrid">The LevelGrid to which this GridObject belongs to.</param>
    /// <param name="gridPosition">The GridPosition of this GridObject.</param>
    /// <param name="visibilityLevel">The visibility of the GridObject.</param>
    public GridObject(LevelGrid levelGrid, GridPosition gridPosition, int visibilityLevel = 0)
    {
        this.levelGrid = levelGrid;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
        this.visibilityLevel = visibilityLevel;
        visibilityLevelType = GridSystemManager.VisibilityLevelType.HIDDEN;
    }

    private void UpdateVisibilityLevelType()
    {
        if (visibilityLevel < 0)
        {
            visibilityLevel = 0;
        }

        if (visibilityLevel > 0)
        {
            visibilityLevelType = GridSystemManager.VisibilityLevelType.VISIBLE;
        }
        else
        {
            visibilityLevelType = GridSystemManager.VisibilityLevelType.DISCOVERED;
        }
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += $"{unit}\n";
        }

        return $"{gridPosition}\n{unitString}{visibilityLevelType}";
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

    /// <summary>
    /// Get the visibility Level enum for this cell.
    /// </summary>
    /// <returns>The visibility of the cell into the grid.</returns>
    public GridSystemManager.VisibilityLevelType GetVisibilityLevelType() => visibilityLevelType;
}
