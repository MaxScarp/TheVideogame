using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviourSystem : MonoBehaviour
{
    private Unit unit;
    private UnitAnimator unitAnimator;
    private BaseBehaviour[] baseBehaviourArray;
    private BehaviourManager.State actualState;

    private float testTimer;
    private float testTimerMax;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        unitAnimator = GetComponent<UnitAnimator>();
        baseBehaviourArray = GetComponents<BaseBehaviour>();

        BehaviourManager.AddBehaviourSystem(unit, this);

        actualState = BehaviourManager.State.IDLE;

        testTimerMax = 5f;
        testTimer = testTimerMax;

        InputManager.OnTakeActionPerformed += InputManager_OnTakeActionPerformed;
    }

    private void Update()
    {
        switch (actualState)
        {
            case BehaviourManager.State.IDLE:
                HandleIdleState();
                break;
            case BehaviourManager.State.MOVE:
                HandleMoveState();
                break;
            case BehaviourManager.State.ATTACK:
                HandleAttackState();
                break;
        }
    }

    private void ChangeStateTo(BehaviourManager.State newState)
    {
        switch (newState)
        {
            case BehaviourManager.State.IDLE:
                Debug.Log("SONO PASSATO IN IDLE");
                testTimer = testTimerMax;
                unitAnimator.StopRunning();
                actualState = newState;
                break;
            case BehaviourManager.State.MOVE:
                Debug.Log("SONO PASSATO IN MOVE");
                unitAnimator.StartRunning();
                actualState = newState;
                break;
            case BehaviourManager.State.ATTACK:
                Debug.Log("SONO PASSATO IN ATTACCO");
                testTimer = testTimerMax;
                actualState = newState;
                break;
        }
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
                    Move(moveBehaviour, raycastHit);
                    ChangeStateTo(BehaviourManager.State.MOVE);
                }
            }
        }
    }

    private void HandleAttackState()
    {
        testTimer -= Time.deltaTime;
        if (testTimer <= 0f)
        {
            ChangeStateTo(BehaviourManager.State.IDLE);
        }
    }

    private void HandleIdleState()
    {
        testTimer -= Time.deltaTime;
        if (testTimer <= 0f)
        {
            ChangeStateTo(BehaviourManager.State.ATTACK);
        }
    }

    private void HandleMoveState()
    {
    }

    private void Move(MoveBehaviour moveBehaviour, RaycastHit raycastHit)
    {
        moveBehaviour.OnStartMoving += MoveBehaviour_OnStartMoving;
        moveBehaviour.OnStopMoving += MoveBehaviour_OnStopMoving;

        moveBehaviour.TakeAction(raycastHit.point);
    }

    private void MoveBehaviour_OnStopMoving(object sender, EventArgs e)
    {
        MoveBehaviour moveBehaviour = sender as MoveBehaviour;

        moveBehaviour.OnStartMoving -= MoveBehaviour_OnStartMoving;
        moveBehaviour.OnStopMoving -= MoveBehaviour_OnStopMoving;

        ChangeStateTo(BehaviourManager.State.IDLE);
    }

    private void MoveBehaviour_OnStartMoving(object sender, EventArgs e)
    {
        ChangeStateTo(BehaviourManager.State.MOVE);
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
