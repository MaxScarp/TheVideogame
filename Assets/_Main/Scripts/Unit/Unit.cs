using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event EventHandler OnUnitSelected;
    public event EventHandler OnUnitDeselected;

    [SerializeField] private bool isEnemy = false;
    [SerializeField] private int levelGridNumber = 0;
    [SerializeField] private int sightRange = 2;

    private bool isSelected;
    private GridPosition gridPosition;
    private GridSystem gridSystem;

    private void Awake()
    {
        isSelected = false;
        gridPosition = new GridPosition();
    }

    private void Start()
    {
        UnitManager.AddUnitToAllUnitList(this);

        SetGridSystem(levelGridNumber);
        SetGridPosition();
    }

    private void Update()
    {
        if (!isEnemy)
        {
            GridPositionHandle();
        }
    }

    private void GridPositionHandle()
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

    private void SetGridSystem(int levelGridNumber)
    {
        if (GridSystemManager.TryGetGridSystem(levelGridNumber, out GridSystem gridSystem))
        {
            this.gridSystem = gridSystem;
        }
    }

    private void SetGridPosition()
    {
        if (GridSystemManager.TryGetGridSystem(levelGridNumber, out GridSystem gridSystem))
        {
            gridPosition = gridSystem.GetLevelGrid().GetGridPosition(transform.position);
            gridSystem.GetLevelGrid().AddUnitAtGridPosition(gridPosition, this);
            gridSystem.GetLevelGrid().UnitMovedGridPosition(this, gridPosition, gridPosition);
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

    /// <summary>
    /// Get the sight range of the unit.
    /// </summary>
    /// <returns>An int representing the cell range sight of the unit.</returns>
    public int GetSightRange() => sightRange;

    /// <summary>
    /// Get the grid position of the unit based on her world position.
    /// </summary>
    /// <returns>A GridPosition struct representing the position of the unit inside the a cell of the grid.</returns>
    public GridPosition GetGridPosition() => gridPosition;
}
