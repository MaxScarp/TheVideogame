using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public event EventHandler OnUnitSelected;
    public event EventHandler OnUnitDeselected;

    [SerializeField] private bool isEnemy;

    private bool isSelected;

    private void Start()
    {
        UnitManager.AddUnitToAllUnitList(this);
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
