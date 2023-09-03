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
    }

    /// <summary>
    /// Get the levelGrid of this GridSystem.
    /// </summary>
    /// <returns></returns>
    public LevelGrid GetLevelGrid() => levelGrid;
}
