using System.Collections.Generic;

public class GridObject
{
    private LevelGrid levelGrid;
    private GridPosition gridPosition;
    private List<Unit> unitList;

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

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public bool HasAnyUnit() => unitList.Count > 0;

    public List<Unit> GetUnitList() => unitList;
}
