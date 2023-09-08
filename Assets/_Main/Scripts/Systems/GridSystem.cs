using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private float cellSize = 1f;

    private LevelGrid levelGrid;

    private void Awake()
    {
        levelGrid = new LevelGrid(width, height, cellSize);

        levelGrid.CreateDebugObject(gridDebugObjectPrefab);
    }

    private void Start()
    {
        GridSystemManager.AddGridSystem(levelGrid.GetLevelNumber(), this);

        levelGrid.OnAnyUnitMovedGridPosition += LevelGrid_OnAnyUnitMovedGridPosition;
    }

    private void LevelGrid_OnAnyUnitMovedGridPosition(object sender, LevelGrid.OnAnyUnitMovedGridPositionEventArgs e)
    {
        UpdateUnitVisibleCells(e.movedUnit, e.oldGridPosition, e.newGridPosition);
    }

    /// <summary>
    /// Get the levelGrid of this GridSystem.
    /// </summary>
    /// <returns>The levelGrid of this GridSystem.</returns>
    public LevelGrid GetLevelGrid() => levelGrid;

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
        for (int x = -unitSightRange; x <= unitSightRange; x++)
        {
            for (int z = -unitSightRange; z <= unitSightRange; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = gridPositionToTest + offsetGridPosition;
                if (!IsValidGridPosition(testGridPosition)) continue;

                GridObject gridObject = levelGrid.GetGridObject(testGridPosition);
                gridObject.VisbilityLevel += valueToAdd;
            }
        }
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
}
