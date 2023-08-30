using System;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        unit.OnUnitSelected += Unit_OnUnitSelected;
        unit.OnUnitDeselected += Unit_OnUnitDeselected;
    }

    private void Start()
    {
        Hide();
    }

    private void Unit_OnUnitDeselected(object sender, EventArgs e)
    {
        Hide();
    }

    private void Unit_OnUnitSelected(object sender, EventArgs e)
    {
        Show();
    }

    /// <summary>
    /// Show the visual object.
    /// </summary>
    private void Show()
    {
        meshRenderer.enabled = true;
    }

    /// <summary>
    /// Hide the visual object.
    /// </summary>
    private void Hide()
    {
        meshRenderer.enabled = false;
    }

    private void OnDestroy()
    {
        unit.OnUnitSelected -= Unit_OnUnitSelected;
        unit.OnUnitDeselected -= Unit_OnUnitDeselected;
    }
}
