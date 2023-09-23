using System.Collections.Generic;

/// <summary>
/// Class that is used for storing data regarding the various GridSystems of the game.
/// </summary>
public static class GridSystemManager
{
    public enum VisibilityLevelType
    {
        HIDDEN,
        DISCOVERED,
        VISIBLE
    }

    private static Dictionary<int, GridSystem> gridSystemDictionary;
    private static List<GridSystem> gridSystemList;
    private static Dictionary<GridSystem, LevelGrid> levelGridDictionary;

    static GridSystemManager()
    {
        gridSystemDictionary = new Dictionary<int, GridSystem>();
        gridSystemList = new List<GridSystem>();
        levelGridDictionary = new Dictionary<GridSystem, LevelGrid>();
    }

    /// <summary>
    /// Add a specified GridSystem to the gridSystemDictionary.
    /// </summary>
    /// <param name="levelGridNumber">The level/layer to which the specified GridSystem belongs to.</param>
    /// <param name="gridSystem">The specified GridSystem.</param>
    public static void AddGridSystem(int levelGridNumber, GridSystem gridSystem, LevelGrid levelGrid)
    {
        if (gridSystemDictionary.ContainsKey(levelGridNumber)) return;

        gridSystemList.Add(gridSystem);
        gridSystemDictionary.Add(levelGridNumber, gridSystem);
        levelGridDictionary.Add(gridSystemDictionary[levelGridNumber], levelGrid);
    }

    /// <summary>
    /// Remove a specified GridSystem to the gridSystemDictionary.
    /// </summary>
    /// <param name="levelGridNumber">The level/layer to which the specified GridSystem belongs to.</param>
    public static void RemoveGridSystem(int levelGridNumber)
    {
        if (!gridSystemDictionary.ContainsKey(levelGridNumber)) return;

        gridSystemList.Remove(gridSystemDictionary[levelGridNumber]);
        levelGridDictionary.Remove(gridSystemDictionary[levelGridNumber]);
        gridSystemDictionary.Remove(levelGridNumber);
    }

    /// <summary>
    /// Get the corresponding GridSystem.
    /// </summary>
    /// <param name="levelGridNumber">The levelGrid number.</param>
    /// <returns>A specified GridSystem if it exists into the gridSystemDictionary, if not returns null</returns>
    public static bool TryGetGridSystem(int levelGridNumber, out GridSystem gridSystem)
    {
        if (gridSystemDictionary.ContainsKey(levelGridNumber))
        {
            gridSystem = gridSystemDictionary[levelGridNumber];
            return true;
        }

        gridSystem = null;
        return false;
    }

    /// <summary>
    /// Get all the GridSystems of the game.
    /// </summary>
    /// <returns>The GridSystem list representing all the GridSystems of the game.</returns>
    public static List<GridSystem> GetGridSystemList() => gridSystemList;

    /// <summary>
    /// Get the levelGrid specified by his levelGridNumber.
    /// </summary>
    /// <param name="levelGridNumber">The levelGridNumber representing the LevelGrid.</param>
    /// <param name="levelGrid">The returned levelGrid.</param>
    /// <returns>True if the LevelGrid has been found, otherwise False.</returns>
    public static bool TryGetLevelGrid(int levelGridNumber, out LevelGrid levelGrid)
    {
        if (TryGetGridSystem(levelGridNumber, out GridSystem gridSystem))
        {
            if (TryGetLevelGrid(gridSystem, out LevelGrid tmpLevelGrid))
            {
                levelGrid = tmpLevelGrid;
                return true;
            }
        }

        levelGrid = null;
        return false;
    }

    /// <summary>
    /// Get the levelGrid specified by the GridSystem linked.
    /// </summary>
    /// <param name="gridSystem">The GridSystem associated to the LevelGrid.</param>
    /// <param name="levelGrid">The returned LevelGrid.</param>
    /// <returns>True if the LevelGrid has been found, otherwise False.</returns>
    public static bool TryGetLevelGrid(GridSystem gridSystem, out LevelGrid levelGrid)
    {
        if (levelGridDictionary.TryGetValue(gridSystem, out LevelGrid tmpLevelGrid))
        {
            levelGrid = tmpLevelGrid;
            return true;
        }

        levelGrid = null;
        return false;
    }
}
