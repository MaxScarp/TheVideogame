using System.Collections.Generic;

public static class UnitManager
{
    private static List<Unit> allUnitList;
    private static List<Unit> friendlyUnitList;
    private static List<Unit> enemyUnitList;
    private static List<Unit> selectedUnitList;

    static UnitManager()
    {
        allUnitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
        selectedUnitList = new List<Unit>();
    }

    public static void AddUnitToAllUnitList(Unit unit)
    {
        allUnitList.Add(unit);

        if (!unit.GetIsEnemy())
        {
            friendlyUnitList.Add(unit);
        }
        else
        {
            enemyUnitList.Add(unit);
        }
    }

    public static void AddSelectedUnit(Unit unit)
    {
        unit.UnitSelected();

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

    public static List<Unit> GetAllUnitList() => allUnitList;

    public static List<Unit> GetFriendlyUnitList() => friendlyUnitList;

    public static List<Unit> GetEnemyUnitList() => enemyUnitList;
}
