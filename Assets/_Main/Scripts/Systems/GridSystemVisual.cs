using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }

    public enum GridVisualType
    {
        BLACK,
        TRANSPARENT
    }

    [SerializeField] private Transform gridObjectVisualPrefab;
    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;
    [SerializeField] private GridSystem gridSystem;

    private GridObjectVisual[,] gridObjectVisualArray;
    private LevelGrid levelGrid;

    private void Awake()
    {
        SetLevelGrid();
    }

    private void Start()
    {
        gridObjectVisualArray = new GridObjectVisual[levelGrid.GetWidth(), levelGrid.GetHeight()];

        for (int x = 0; x < levelGrid.GetWidth(); x++)
        {
            for (int z = 0; z < levelGrid.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridObjectVisualTransform = Instantiate(gridObjectVisualPrefab, levelGrid.GetWorldPosition(gridPosition), Quaternion.identity);
                GridObjectVisual gridObjectVisual = gridObjectVisualTransform.GetComponent<GridObjectVisual>();
                gridObjectVisual.AdjustScale(gridSystem.GetCellSize());
                gridObjectVisualArray[x, z] = gridObjectVisual;
            }
        }

        gridSystem.OnGridVisibilityUpdated += GridSystem_OnGridVisibilityUpdated;
    }

    private void GridSystem_OnGridVisibilityUpdated(object sender, GridSystem.OnGridVisibilityUpdatedEventArgs e)
    {
        UpdateGridVisual(e.gridObjectList);
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }

        Debug.LogError($"Could not find GridVisualTypeMaterial for GridVisualType {gridVisualType}");
        return null;
    }

    private void UpdateGridVisual(List<GridObject> updatedGridObjectList)
    {
        foreach (GridObject gridObject in updatedGridObjectList)
        {
            GridObjectVisual gridObjectVisual = GetGridObjectVisual(gridObject.GetGridPosition());
            switch (gridObject.GetVisibilityLevelType())
            {
                case GridSystemManager.VisibilityLevelType.HIDDEN:
                    gridObjectVisual.UpdateMaterial(GetGridVisualTypeMaterial(GridVisualType.BLACK));
                    break;
                case GridSystemManager.VisibilityLevelType.DISCOVERED:
                    gridObjectVisual.UpdateMaterial(GetGridVisualTypeMaterial(GridVisualType.TRANSPARENT));
                    break;
                case GridSystemManager.VisibilityLevelType.VISIBLE:
                    gridObjectVisual.Hide();
                    break;
            }
        }
    }

    private GridObjectVisual GetGridObjectVisual(GridPosition gridPosition) => gridObjectVisualArray[gridPosition.X, gridPosition.Z];

    private void SetLevelGrid()
    {
        if (GridSystemManager.TryGetLevelGrid(gridSystem, out LevelGrid levelGrid))
        {
            this.levelGrid = levelGrid;
        }
    }

    private void OnDestroy()
    {
        gridSystem.OnGridVisibilityUpdated -= GridSystem_OnGridVisibilityUpdated;
    }
}
