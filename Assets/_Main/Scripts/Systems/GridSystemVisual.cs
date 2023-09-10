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

    private void Start()
    {
        gridObjectVisualArray = new GridObjectVisual[gridSystem.GetLevelGrid().GetWidth(), gridSystem.GetLevelGrid().GetHeight()];

        for (int x = 0; x < gridSystem.GetLevelGrid().GetWidth(); x++)
        {
            for (int z = 0; z < gridSystem.GetLevelGrid().GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridObjectVisualTransform = Instantiate(gridObjectVisualPrefab, gridSystem.GetLevelGrid().GetWorldPosition(gridPosition), Quaternion.identity);
                gridObjectVisualArray[x, z] = gridObjectVisualTransform.GetComponent<GridObjectVisual>();
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
}
