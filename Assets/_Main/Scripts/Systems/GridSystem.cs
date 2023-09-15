using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public event EventHandler<OnGridVisibilityUpdatedEventArgs> OnGridVisibilityUpdated;
    public class OnGridVisibilityUpdatedEventArgs : EventArgs
    {
        public List<GridObject> gridObjectList;
    }

    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private int levelNumber = 0;

    private LevelGrid levelGrid;

    private void Awake()
    {
        levelGrid = new LevelGrid(width, height, cellSize);

        levelGrid.CreateDebugObject(gridDebugObjectPrefab);

        GridSystemManager.AddGridSystem(levelNumber, this, levelGrid);
    }

    private void Start()
    {
        levelGrid.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;
    }

    private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, LevelGrid.OnAnyUnitMovedGridPositionEventArgs e)
    {
        UpdateUnitVisibleCells(e.movedUnit, e.oldGridPosition, e.newGridPosition);
    }

    private void UpdateUnitVisibleCells(Unit unit, GridPosition oldGridPosition, GridPosition newGridPosition)
    {
        //Subtract 1 to old cells visibility
        int valueToAddForVisibility = -1;
        UpdateSurroundingCells(unit.GetSightRange(), oldGridPosition, valueToAddForVisibility);

        //Add 1 to new cells visibility
        valueToAddForVisibility = 1;
        UpdateSurroundingCells(unit.GetSightRange(), newGridPosition, valueToAddForVisibility);
    }

    private void UpdateSurroundingCells(int unitSightRange, GridPosition gridPositionToTest, int valueToAdd)
    {
        List<GridObject> gridObjectList = new List<GridObject>();

        for (int x = -unitSightRange; x <= unitSightRange; x++)
        {
            for (int z = -unitSightRange; z <= unitSightRange; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = gridPositionToTest + offsetGridPosition;
                if (!IsValidGridPosition(testGridPosition)) continue;

                GridObject gridObject = levelGrid.GetGridObject(testGridPosition);
                gridObject.VisbilityLevel += valueToAdd;

                gridObjectList.Add(gridObject);
            }
        }

        OnGridVisibilityUpdated?.Invoke(this, new OnGridVisibilityUpdatedEventArgs { gridObjectList = gridObjectList });
    }

    /// <summary>
    /// Check if the grid position is inside the grid or not.
    /// </summary>
    /// <param name="gridPosition">GridPosition to be tested.</param>
    /// <returns>True if the GridPosition is inside the grid, otherwise False.</returns>
    private bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.X >= 0 && gridPosition.Z >= 0 && gridPosition.X < width && gridPosition.Z < height;
    }

    private void OnDestroy()
    {
        levelGrid.OnAnyUnitMovedGridPosition -= LevelGrid_OnAnyUnitMovedGridPosition;
    }
}
