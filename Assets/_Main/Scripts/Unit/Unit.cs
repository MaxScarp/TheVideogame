using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event EventHandler OnUnitSelected;
    public event EventHandler OnUnitDeselected;

    [SerializeField] private bool isEnemy = false;
    [SerializeField] private int levelGridNumber = 0;

    private bool isSelected;
    private GridPosition gridPosition;

    private void Start()
    {
        UnitManager.AddUnitToAllUnitList(this);

        SetGridPosition();
    }

    private void Update()
    {
        if (GridSystemManager.TryGetGridSystem(levelGridNumber, out GridSystem gridSystem))
        {
            GridPosition newGridPosition = gridSystem.GetLevelGrid().GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                //Unit changed GridPosition
                GridPosition oldGridPosition = gridPosition;
                gridPosition = newGridPosition;
                gridSystem.GetLevelGrid().UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
            }
        }
    }

    private void SetGridPosition()
    {
        if (GridSystemManager.TryGetGridSystem(levelGridNumber, out GridSystem gridSystem))
        {
            gridPosition = gridSystem.GetLevelGrid().GetGridPosition(transform.position);
            gridSystem.GetLevelGrid().AddUnitAtGridPosition(gridPosition, this);
        }
    }

    /// <summary>
    /// Set the current unit as a selected unit.
    /// </summary>
    public void UnitSelected()
    {
        isSelected = true;

        OnUnitSelected?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Set the current unit as a not selected unit.
    /// </summary>
    public void UnitDeselected()
    {
        isSelected = false;

        OnUnitDeselected?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Get the isSelected.
    /// </summary>
    /// <returns>True if the Unit is selected, otherwise False.</returns>
    public bool GetIsSelected() => isSelected;

    /// <summary>
    /// Get the isEnemy.
    /// </summary>
    /// <returns>True if the Unit is an enemy Unit, otherwise False.</returns>
    public bool GetIsEnemy() => isEnemy;
}
