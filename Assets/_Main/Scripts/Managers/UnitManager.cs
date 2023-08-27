using System.Collections.Generic;

public static class UnitManager
{
    private static List<Unit> friendlyUnitList;
    private static List<Unit> selectedUnitList;

    static UnitManager()
    {
        selectedUnitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
    }

    public static void AddSelectedUnit(Unit unit)
    {
        unit.UnitSelected();

        if (!unit.GetIsEnemy())
        {
            friendlyUnitList.Add(unit);
        }

        selectedUnitList.Add(unit);
    }

    public static void RemoveSelectedUnit(Unit unit)
    {
        unit.UnitDeselected();

        selectedUnitList.Remove(unit);
    }

    public static void ClearSelectedUnitList()
    {
        foreach (Unit unit in selectedUnitList)
        {
            unit.UnitDeselected();
        }

        selectedUnitList.Clear();
    }

    public static List<Unit> GetSelectedUnitList() => selectedUnitList;

    public static List<Unit> GetFriendlyUnitList() => friendlyUnitList;
}
