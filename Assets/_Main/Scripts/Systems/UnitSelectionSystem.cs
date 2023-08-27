using System;
using UnityEngine;

public class UnitSelectionSystem : MonoBehaviour
{
    [SerializeField] private LayerMask unitLayerMask;
    [SerializeField] private RectTransform selectionBoxVisual;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rect selectionBox;

    private void Awake()
    {
        UpdateSelectionBoxVisual();

        InputManager.OnSelectUnitSinglePerformed += InputManager_OnSelectUnitSinglePerformed;
        InputManager.OnSelectUnitMultipleStarted += InputManager_OnSelectUnitMultipleStarted;
    }

    private void Update()
    {
        if (InputManager.IsMouseLeftHeld())
        {
            endPosition = UtilsClass.GetMouseScreenPosition();

            UpdateSelectionBoxVisual();
            UpdateSelectionBox();
        }
        else
        {
            SelectMultipleUnits();

            startPosition = Vector3.zero;
            endPosition = Vector3.zero;

            UpdateSelectionBoxVisual();
        }
    }

    private void InputManager_OnSelectUnitMultipleStarted(object sender, EventArgs e)
    {
        startPosition = UtilsClass.GetMouseScreenPosition();
        selectionBox = new Rect();
    }

    private void InputManager_OnSelectUnitSinglePerformed(object sender, EventArgs e)
    {
        SelectClickedUnit();
    }

    private void UpdateSelectionBox()
    {
        if (UtilsClass.GetMouseScreenPosition().x < startPosition.x)
        {
            selectionBox.xMin = UtilsClass.GetMouseScreenPosition().x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = UtilsClass.GetMouseScreenPosition().x;
        }

        if (UtilsClass.GetMouseScreenPosition().y < startPosition.y)
        {
            selectionBox.yMin = UtilsClass.GetMouseScreenPosition().y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = UtilsClass.GetMouseScreenPosition().y;
        }
    }

    /// <summary>
    /// Select all the units under the selection box
    /// </summary>
    private void SelectMultipleUnits()
    {
        foreach (Unit unit in UnitManager.GetFriendlyUnitList())
        {
            if (selectionBox.Contains(UtilsClass.GetWorldToScreenPosition(unit.transform.position)))
            {
                if (!unit.GetIsSelected())
                {
                    UnitManager.AddSelectedUnit(unit);
                }
                else
                {
                    UnitManager.RemoveSelectedUnit(unit);
                }
            }
        }
    }

    private void UpdateSelectionBoxVisual()
    {
        Vector2 boxStart = new Vector2(startPosition.x, startPosition.y);
        Vector2 boxEnd = new Vector2(endPosition.x, endPosition.y);

        Vector2 boxCenter = (boxStart + boxEnd) * 0.5f;
        selectionBoxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        selectionBoxVisual.sizeDelta = boxSize;
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
                        UnitManager.AddSelectedUnit(unit);
                    }
                    else
                    {
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
