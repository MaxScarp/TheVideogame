using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        selectMultipleUnitHeldTimerMax = 0.085f;

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

            endPosition = GetMouseScreenPosition();

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
    /// Get the mouse position on the screen.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseScreenPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 0f;

        return mousePosition;
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
        startPosition = GetMouseScreenPosition();
    }

    private void InputManager_OnSelectUnitSinglePerformed(object sender, EventArgs e)
    {
        SelectClickedUnit();
    }

    /// <summary>
    /// Select all the units under the selection box
    /// </summary>
    private int SelectMultipleUnits()
    {
        List<Unit> unitInsideSelectionBox = GetUnitInsideSelectionBox();

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
                        UnitManager.AddUnitToAllSelectedUnitList(unit);
                    }
                }
            }
            else
            {
                //SelectUnitInclusive is not held
                UnitManager.ClearAllSelectedUnitList();
                foreach (Unit unit in unitInsideSelectionBox)
                {
                    UnitManager.AddUnitToAllSelectedUnitList(unit);
                }
            }
        }
        else
        {
            //No unit is inside the selectionBox
            if (!InputManager.IsInclusive())
            {
                UnitManager.ClearAllSelectedUnitList();
            }
        }
        return unitInsideSelectionBox.Count;
    }

    private List<Unit> GetUnitInsideSelectionBox()
    {
        List<Unit> unitInsideSelectionBox = new List<Unit>();

        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta * 0.5f);
        Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta * 0.5f);

        foreach (Unit unit in UnitManager.GetAllUnitList())
        {
            if (unit.GetIsEnemy()) continue;

            Vector3 unitScreenPosition = Camera.main.WorldToScreenPoint(unit.transform.position);
            if (unitScreenPosition.x > min.x && unitScreenPosition.x < max.x && unitScreenPosition.y > min.y && unitScreenPosition.y < max.y)
            {
                //The unit is inside the selectionBox
                unitInsideSelectionBox.Add(unit);
            }
        }

        return unitInsideSelectionBox;
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
        Ray ray = Camera.main.ScreenPointToRay(GetMouseScreenPosition());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Unit unit))
            {
                //Return condition if the selected unit is an enemy
                if (unit.GetIsEnemy())
                {
                    if (!InputManager.IsInclusive())
                    {
                        UnitManager.ClearAllSelectedUnitList();
                    }

                    return;
                }

                //A unit has been hit
                if (InputManager.IsInclusive())
                {
                    //SelectUnitInclusive is held
                    if (!unit.GetIsSelected())
                    {
                        //Unit is not already selected
                        UnitManager.AddUnitToAllSelectedUnitList(unit);
                    }
                    else
                    {
                        //Unit is already selected
                        UnitManager.RemoveUnitFromAllSelectedUnitList(unit);
                    }
                }
                else
                {
                    //SelectUnitInclusive is not held
                    UnitManager.ClearAllSelectedUnitList();
                    UnitManager.AddUnitToAllSelectedUnitList(unit);
                }
            }
        }
        else
        {
            //No unit has been hit
            if (!InputManager.IsInclusive())
            {
                UnitManager.ClearAllSelectedUnitList();
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.OnSelectUnitSinglePerformed -= InputManager_OnSelectUnitSinglePerformed;
        InputManager.OnSelectUnitMultipleStarted -= InputManager_OnSelectUnitMultipleStarted;
    }
}
