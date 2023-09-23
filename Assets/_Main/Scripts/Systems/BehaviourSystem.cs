using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviourSystem : MonoBehaviour
{
    private Unit unit;
    private BaseBehaviour[] baseBehaviourArray;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        baseBehaviourArray = GetComponents<BaseBehaviour>();

        BehaviourManager.AddBehaviourSystem(unit, this);

        InputManager.OnTakeActionPerformed += InputManager_OnTakeActionPerformed;
    }

    private void InputManager_OnTakeActionPerformed(object sender, EventArgs e)
    {
        foreach (Unit unit in UnitManager.GetFriendlySelectedUnitList())
        {
            if (this.unit != unit) continue;

            //Move all selected units.
            if (TryGetBehaviour(out MoveBehaviour moveBehaviour))
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, moveBehaviour.GetMousePlaneLayerMask()))
                {
                    moveBehaviour.TakeAction(raycastHit.point);
                }
            }
        }
    }

    /// <summary>
    /// Try to get the specified Behaviour Component.
    /// </summary>
    /// <typeparam name="T">The class of the specified Behaviour.</typeparam>
    /// <param name="behaviour">The Behaviour that has been requested to be found.</param>
    /// <returns>True if the Behaviour Component has been found otherwise False.</returns>
    private bool TryGetBehaviour<T>(out T behaviour) where T : BaseBehaviour
    {
        foreach (BaseBehaviour baseBehaviour in baseBehaviourArray)
        {
            if (baseBehaviour is T)
            {
                behaviour = (T)baseBehaviour;
                return true;
            }
        }

        behaviour = null;
        return false;
    }

    /// <summary>
    /// Get the baseBehaviourArray.
    /// </summary>
    /// <returns>A baseBehaviourArray representing all the BaseBehaviours that a Unit owns.</returns>
    public BaseBehaviour[] GetBaseBehaviourArray() => baseBehaviourArray;

    private void OnDestroy()
    {
        InputManager.OnTakeActionPerformed -= InputManager_OnTakeActionPerformed;
    }
}
