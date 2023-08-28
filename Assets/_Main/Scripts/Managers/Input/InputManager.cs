using System;
using UnityEngine.InputSystem;

public static class InputManager
{
    public static event EventHandler OnSelectUnitSinglePerformed;
    public static event EventHandler OnSelectUnitMultipleStarted;

    private static UnitInputActions unitInputActions;

    static InputManager()
    {
        unitInputActions = new UnitInputActions();

        unitInputActions.Unit.Enable();

        unitInputActions.Unit.SelectUnitSingle.performed += SelectUnitSingle_performed;
        unitInputActions.Unit.SelectUnitMultiple.started += SelectUnitMultiple_started;
    }

    private static void SelectUnitMultiple_started(InputAction.CallbackContext obj)
    {
        OnSelectUnitMultipleStarted?.Invoke(null, EventArgs.Empty);
    }

    private static void SelectUnitSingle_performed(InputAction.CallbackContext obj)
    {
        OnSelectUnitSinglePerformed?.Invoke(null, EventArgs.Empty);
    }

    public static bool IsInclusive() => unitInputActions.Unit.SelectUnitInclusive.ReadValue<float>() > 0f;

    public static bool IsSelectUnitMultipleHeld() => unitInputActions.Unit.SelectUnitMultiple.ReadValue<float>() > 0f;

    public static bool IsSelectUnitMultipleReleased() => unitInputActions.Unit.SelectUnitMultiple.WasReleasedThisFrame();
}
