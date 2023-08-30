using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;

    private LevelGrid levelGrid;

    private void Awake()
    {
        levelGrid = new LevelGrid(10, 10, 1f);
        levelGrid.CreateDebugObject(gridDebugObjectPrefab);
    }
}
