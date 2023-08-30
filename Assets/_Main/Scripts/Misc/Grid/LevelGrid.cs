using UnityEngine;

public class LevelGrid
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray;

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

    public Vector3 GetWorldPosition(GridPosition gridPosition) => new Vector3(gridPosition.X, 0f, gridPosition.Z) * cellSize;

    public GridPosition GetGridPosition(Vector3 worldPosition) => new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));

    public GridObject GetGridObject(GridPosition gridPosition) => gridObjectArray[gridPosition.X, gridPosition.Z];
}
