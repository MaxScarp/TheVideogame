using UnityEngine;

/// <summary>
/// Class that represents a grid on the world.
/// </summary>
public class LevelGrid
{
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
}
