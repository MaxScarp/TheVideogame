using System;
using UnityEngine;

/// <summary>
/// Class that represents a grid on the world.
/// </summary>
public class LevelGrid
{
    public event EventHandler<OnAnyUnitMovedGridPositionEventArgs> OnAnyUnitMovedGridPosition;

    public class OnAnyUnitMovedGridPositionEventArgs : EventArgs
    {
        public Unit movedUnit;
        public GridPosition oldGridPosition;
        public GridPosition newGridPosition;
    }

    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

    /// <summary>
    /// Constructor of the LevelGrid class.
    /// </summary>
    /// <param name="width">Width of the grid.</param>
    /// <param name="height">Height of the grid.</param>
    /// <param name="cellSize">The size of a cell inside the grid.</param>
    public LevelGrid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < this.width; x++)
        {
            for (int z = 0; z < this.height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }

    /// <summary>
    /// Create all the GridDebugObjects and assigns all the GridObjects. (Draw the grid on the world)
    /// </summary>
    /// <param name="debugPrefab"></param>
    public void CreateDebugObject(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    /// <summary>
    /// To use once the specified Unit has moved from a GridPositon to another GridPosition.
    /// </summary>
    /// <param name="unit">The specified Unit.</param>
    /// <param name="fromGridPosition">The old GridPositon.</param>
    /// <param name="toGridPosition">The new GridPosition.</param>
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);

        OnAnyUnitMovedGridPosition?.Invoke(this, new OnAnyUnitMovedGridPositionEventArgs { movedUnit = unit, oldGridPosition = fromGridPosition, newGridPosition = toGridPosition });
    }

    /// <summary>
    /// Remove a specified Unit into a specified GridPosition.
    /// </summary>
    /// <param name="gridPosition">Specified GridPosition.</param>
    /// <param name="unit">Specified Unit.</param>
    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    /// <summary>
    /// Add a specified Unit into a specified GridPosition.
    /// </summary>
    /// <param name="gridPosition">Specified GridPosition.</param>
    /// <param name="unit">Specified Unit.</param>
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    /// <summary>
    /// Convert a GridPosition into a world position.
    /// </summary>
    /// <param name="gridPosition">The GridPosition to convert.</param>
    /// <returns>A Vector3 representing the world coordinate for the specified GridPosition.</returns>
    public Vector3 GetWorldPosition(GridPosition gridPosition) => new Vector3(gridPosition.X, 0f, gridPosition.Z) * cellSize;

    /// <summary>
    /// Convert a Vector3 into a GridPosition.
    /// </summary>
    /// <param name="worldPosition">The Vector3 to convert.</param>
    /// <returns>A GridPosition representing a position inside the grid of the LevelGrid.</returns>
    public GridPosition GetGridPosition(Vector3 worldPosition) => new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));

    /// <summary>
    /// Get the GridObject at a specified GridPosition.
    /// </summary>
    /// <param name="gridPosition">The GridPosition that "owns" the GridObject.</param>
    /// <returns>The GridObject that is inside the grid of the LevelGrid at the specified GridPosition</returns>
    public GridObject GetGridObject(GridPosition gridPosition) => gridObjectArray[gridPosition.X, gridPosition.Z];

    /// <summary>
    /// Get the width of the LevelGrid.
    /// </summary>
    /// <returns>An integer representing the width of the Grid.</returns>
    public int GetWidth() => width;

    /// <summary>
    /// Get the height of the LevelGrid.
    /// </summary>
    /// <returns>An integer representing the height of the Grid.</returns>
    public int GetHeight() => height;
}
