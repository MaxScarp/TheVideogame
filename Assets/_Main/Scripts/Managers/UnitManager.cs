using System.Collections.Generic;

public static class UnitManager
{
    private static List<Unit> allUnitList;
    private static List<Unit> friendlyUnitList;
    private static List<Unit> enemyUnitList;
    private static List<Unit> allSelectedUnitList;
    private static List<Unit> friendlySelectedUnitList;
    private static List<Unit> enemySelectedUnitList;

    /// <summary>
    /// This class is the container of all the units of the game.
    /// </summary>
    static UnitManager()
    {
        allUnitList = new List<Unit>();
        friendlyUnitList = new List<Unit>();
        enemyUnitList = new List<Unit>();
        allSelectedUnitList = new List<Unit>();
        friendlySelectedUnitList = new List<Unit>();
        enemySelectedUnitList = new List<Unit>();
    }

    /// <summary>
    /// Add the specified Unit to the AllUnitList.
    /// </summary>
    /// <param name="unit">Specified Unit.</param>
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

    /// <summary>
    /// Add the specified Unit to the AllSelectedUnitList.
    /// </summary>
    /// <param name="unit">Specified Unit.</param>
    public static void AddUnitToAllSelectedUnitList(Unit unit)
    {
        unit.UnitSelected();

        allSelectedUnitList.Add(unit);

        if (!unit.GetIsEnemy())
        {
            friendlySelectedUnitList.Add(unit);
        }
        else
        {
            enemySelectedUnitList.Add(unit);
        }
    }

    /// <summary>
    /// Remove the specified Unit from the AllSelectedUnitList.
    /// </summary>
    /// <param name="unit">Specified Unit.</param>
    public static void RemoveUnitFromAllSelectedUnitList(Unit unit)
    {
        unit.UnitDeselected();

        allSelectedUnitList.Remove(unit);

        if (!unit.GetIsEnemy())
        {
            friendlySelectedUnitList.Remove(unit);
        }
        else
        {
            enemySelectedUnitList.Remove(unit);
        }
    }

    /// <summary>
    /// Clear completely the AllSelectedUnitList.
    /// </summary>
    public static void ClearAllSelectedUnitList()
    {
        foreach (Unit unit in allSelectedUnitList)
        {
            unit.UnitDeselected();
        }

        foreach (Unit unit in friendlySelectedUnitList)
        {
            unit.UnitDeselected();
        }

        foreach (Unit unit in enemySelectedUnitList)
        {
            unit.UnitDeselected();
        }

        allSelectedUnitList.Clear();
        friendlySelectedUnitList.Clear();
        enemySelectedUnitList.Clear();
    }

    public static List<Unit> GetAllSelectedUnitList() => allSelectedUnitList;

    public static List<Unit> GetAllUnitList() => allUnitList;

    public static List<Unit> GetFriendlyUnitList() => friendlyUnitList;

    public static List<Unit> GetFriendlySelectedUnitList() => friendlySelectedUnitList;

    public static List<Unit> GetEnemyUnitList() => enemyUnitList;

    public static List<Unit> GetEnemySelectedUnitList() => enemySelectedUnitList;
}
