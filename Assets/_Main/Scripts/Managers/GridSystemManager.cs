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

    static GridSystemManager()
    {
        gridSystemDictionary = new Dictionary<int, GridSystem>();
        gridSystemList = new List<GridSystem>();
    }

    /// <summary>
    /// Add a specified GridSystem to the gridSystemDictionary.
    /// </summary>
    /// <param name="levelGridNumber">The level/layer to which the specified GridSystem belongs to.</param>
    /// <param name="gridSystem">The specified GridSystem.</param>
    public static void AddGridSystem(int levelGridNumber, GridSystem gridSystem)
    {
        if (gridSystemDictionary.ContainsKey(levelGridNumber)) return;

        gridSystemList.Add(gridSystem);
        gridSystemDictionary.Add(levelGridNumber, gridSystem);
    }

    /// <summary>
    /// Remove a specified GridSystem to the gridSystemDictionary.
    /// </summary>
    /// <param name="levelGridNumber">The level/layer to which the specified GridSystem belongs to.</param>
    public static void RemoveGridSystem(int levelGridNumber)
    {
        if (!gridSystemDictionary.ContainsKey(levelGridNumber)) return;

        gridSystemList.Remove(gridSystemDictionary[levelGridNumber]);
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
}
