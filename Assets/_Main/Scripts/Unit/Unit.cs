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
        UnitManager.AddSelectedUnit(this);
    }

    public void UnitSelected()
    {
        isSelected = true;

        OnUnitSelected?.Invoke(this, EventArgs.Empty);
    }

    public void UnitDeselected()
    {
        isSelected = false;

        OnUnitDeselected?.Invoke(this, EventArgs.Empty);
    }

    public bool GetIsSelected() => isSelected;

    public bool GetIsEnemy() => isEnemy;
}
