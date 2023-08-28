using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private RectTransform unitSelectionArea;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private float selectMultipleUnitHeldTimer;
    private float selectMultipleUnitHeldTimerMax;
    private bool isMouseBeenHeld;

    private void Awake()
    {
        selectMultipleUnitHeldTimerMax = 0.1f;

        InputManager.OnSelectUnitSinglePerformed += InputManager_OnSelectUnitSinglePerformed;
        InputManager.OnSelectUnitMultipleStarted += InputManager_OnSelectUnitMultipleStarted;
    }

    private void Start()
    {
        UpdateUnitSelectionArea();
    }

    private void Update()
    {
        if (InputManager.IsSelectUnitMultipleHeld())
        {
            selectMultipleUnitHeldTimer += Time.deltaTime;
            isMouseBeenHeld = selectMultipleUnitHeldTimer >= selectMultipleUnitHeldTimerMax;

            endPosition = UtilsClass.GetMouseScreenPosition();

            UpdateUnitSelectionArea();
        }

        if (InputManager.IsSelectUnitMultipleReleased())
        {
            if (isMouseBeenHeld)
            {
                SelectMultipleUnits();
            }
            ResetSelectMultipleUnitHeldTimer();
            ResetMousePosition();
            UpdateUnitSelectionArea();
        }
    }

    /// <summary>
    /// Reset the startPosition and the endPosition
    /// </summary>
    private void ResetMousePosition()
    {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
    }

    /// <summary>
    /// Reset the timer used for revealing the mouse held for selecting multiple units
    /// </summary>
    private void ResetSelectMultipleUnitHeldTimer()
    {
        selectMultipleUnitHeldTimer = 0f;
        isMouseBeenHeld = false;
    }

    private void InputManager_OnSelectUnitMultipleStarted(object sender, EventArgs e)
    {
        startPosition = UtilsClass.GetMouseScreenPosition();
    }

    private void InputManager_OnSelectUnitSinglePerformed(object sender, EventArgs e)
    {
        ResetSelectMultipleUnitHeldTimer();

        SelectClickedUnit();
    }

    /// <summary>
    /// Select all the units under the selection box
    /// </summary>
    private void SelectMultipleUnits()
    {
        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta * 0.5f);
        Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta * 0.5f);

        List<Unit> unitInsideSelectionBox = new List<Unit>();

        foreach (Unit unit in UnitManager.GetAllUnitList())
        {
            Vector3 unitScreenPosition = UtilsClass.GetWorldToScreenPosition(unit.transform.position);
            if (unitScreenPosition.x > min.x && unitScreenPosition.x < max.x && unitScreenPosition.y > min.y && unitScreenPosition.y < max.y)
            {
                //The unit is inside the selectionBox
                unitInsideSelectionBox.Add(unit);
            }
        }

        if (unitInsideSelectionBox.Count > 0)
        {
            //At least one unit is inside the selectionBox
            if (InputManager.IsInclusive())
            {
                //SelectUnitInclusive is held
                foreach (Unit unit in unitInsideSelectionBox)
                {
                    if (!unit.GetIsSelected())
                    {
                        //Unit is not already selected
                        UnitManager.AddSelectedUnit(unit);
                    }
                }
            }
            else
            {
                //SelectUnitInclusive is not held
                UnitManager.ClearSelectedUnitList();
                foreach (Unit unit in unitInsideSelectionBox)
                {
                    UnitManager.AddSelectedUnit(unit);
                }
            }
        }
        else
        {
            //No unit is inside the selectionBox
            if (!InputManager.IsInclusive())
            {
                UnitManager.ClearSelectedUnitList();
            }
        }
    }

    /// <summary>
    /// Update the visual of the box for selecting multiple units
    /// </summary>
    private void UpdateUnitSelectionArea()
    {
        float areaWidth = endPosition.x - startPosition.x;
        float areaHeight = endPosition.y - startPosition.y;

        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        unitSelectionArea.anchoredPosition = startPosition + new Vector3(areaWidth * 0.5f, areaHeight * 0.5f, 0f);
    }

    /// <summary>
    /// Select a unit that has been clicked
    /// </summary>
    private void SelectClickedUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(UtilsClass.GetMouseScreenPosition());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Unit unit))
            {
                //A unit has been hit
                if (InputManager.IsInclusive())
                {
                    //SelectUnitInclusive is held
                    if (!unit.GetIsSelected())
                    {
                        //Unit is not already selected
                        UnitManager.AddSelectedUnit(unit);
                    }
                    else
                    {
                        //Unit is already selected
                        UnitManager.RemoveSelectedUnit(unit);
                    }
                }
                else
                {
                    //SelectUnitInclusive is not held
                    UnitManager.ClearSelectedUnitList();
                    UnitManager.AddSelectedUnit(unit);
                }
            }
        }
        else
        {
            //No unit has been hit
            if (!InputManager.IsInclusive())
            {
                UnitManager.ClearSelectedUnitList();
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.OnSelectUnitSinglePerformed -= InputManager_OnSelectUnitSinglePerformed;
        InputManager.OnSelectUnitMultipleStarted -= InputManager_OnSelectUnitMultipleStarted;
    }
}
