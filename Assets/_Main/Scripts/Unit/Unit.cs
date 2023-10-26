using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event EventHandler OnUnitSelected;
    public event EventHandler OnUnitDeselected;

    [SerializeField] private bool isEnemy = false;
    [SerializeField] private int levelGridNumber = 0;
    [SerializeField] private int sightRange = 2;
    [SerializeField] private GameObject unit3DVisual;

    [SerializeField] private StatisticsSO statisticVitality;
    [SerializeField] private StatisticsSO statisticStrenght;
    [SerializeField] private StatisticsSO statisticDexterity;
    [SerializeField] private StatisticsSO statisticIntelligence;

    private bool isSelected;
    private GridPosition gridPosition;
    private LevelGrid levelGrid;

    private void Awake()
    {
        isSelected = false;
        gridPosition = new GridPosition();
    }

    private void Start()
    {
        UnitManager.AddUnitToAllUnitList(this);

        SetLevelGrid(levelGridNumber);
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
        GridPosition newGridPosition = levelGrid.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            //Unit changed GridPosition
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPosition;
            levelGrid.UnitMovedGridPosition(this, oldGridPosition, newGridPosition);
        }
    }

    private void SetLevelGrid(int levelGridNumber)
    {
        if (GridSystemManager.TryGetLevelGrid(levelGridNumber, out LevelGrid levelGrid))
        {
            this.levelGrid = levelGrid;
        }
    }

    private void SetGridPosition()
    {
        gridPosition = levelGrid.GetGridPosition(transform.position);
        levelGrid.AddUnitAtGridPosition(gridPosition, this);
        levelGrid.UnitMovedGridPosition(this, gridPosition, gridPosition);
    }

    /// <summary>
    /// Show the 3D rendering of the Unit.
    /// </summary>
    public void Show()
    {
        unit3DVisual.SetActive(true);
    }

    /// <summary>
    /// Hide the 3D rendering of the Unit.
    /// </summary>
    public void Hide()
    {
        unit3DVisual.SetActive(false);
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

    public StatisticsSO GetStatisticVitality() => statisticVitality;
    public StatisticsSO GetStatisticStrenght() => statisticStrenght;
    public StatisticsSO GetStatisticDexterity() => statisticDexterity;
    public StatisticsSO GetStatisticIntelligence() => statisticIntelligence;
}
