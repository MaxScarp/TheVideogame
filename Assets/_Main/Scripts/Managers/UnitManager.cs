using System.Collections.Generic;

/// <summary>
/// Class that is used for storing all the Units of the game.
/// </summary>
public static class UnitManager
{
    private static List<Unit> allUnitList;
    private static List<Unit> friendlyUnitList;
    private static List<Unit> enemyUnitList;
    private static List<Unit> allSelectedUnitList;
    private static List<Unit> friendlySelectedUnitList;
    private static List<Unit> enemySelectedUnitList;

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

    /// <summary>
    /// Get the AllSelectedUnitList.
    /// </summary>
    /// <returns>A list that contains all the Units that are currently selected.</returns>
    public static List<Unit> GetAllSelectedUnitList() => allSelectedUnitList;

    /// <summary>
    /// Get the AllUnitList.
    /// </summary>
    /// <returns>A list that contains all the Units of the game.</returns>
    public static List<Unit> GetAllUnitList() => allUnitList;

    /// <summary>
    /// Get the FriendlyUnitList.
    /// </summary>
    /// <returns>A list that contains all the friendly Units of the game.</returns>
    public static List<Unit> GetFriendlyUnitList() => friendlyUnitList;

    /// <summary>
    /// Get the FriendlySelectedUnitList.
    /// </summary>
    /// <returns>A list that contains all the friendly Units that are currently selected.</returns>
    public static List<Unit> GetFriendlySelectedUnitList() => friendlySelectedUnitList;

    /// <summary>
    /// Get the EnemyUnitList.
    /// </summary>
    /// <returns>A list that contains all the enemy Units of the game.</returns>
    public static List<Unit> GetEnemyUnitList() => enemyUnitList;

    /// <summary>
    /// Get the EnemySelectedUnitList.
    /// </summary>
    /// <returns>A list that contains all the enemy Units that are currently selected.</returns>
    public static List<Unit> GetEnemySelectedUnitList() => enemySelectedUnitList;
}
