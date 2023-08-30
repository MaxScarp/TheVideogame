using System;
using UnityEngine.InputSystem;

/// <summary>
/// Class that is used as an event dispatcher for the inputs from the player.
/// </summary>
public static class InputManager
{
    public static event EventHandler OnSelectUnitSinglePerformed;
    public static event EventHandler OnSelectUnitMultipleStarted;
    public static event EventHandler OnTakeActionPerformed;

    private static UnitInputActions unitInputActions;

    static InputManager()
    {
        unitInputActions = new UnitInputActions();

        unitInputActions.Unit.Enable();

        unitInputActions.Unit.SelectUnitSingle.performed += SelectUnitSingle_performed;
        unitInputActions.Unit.SelectUnitMultiple.started += SelectUnitMultiple_started;
        unitInputActions.Unit.TakeAction.performed += TakeAction_performed;
    }

    private static void TakeAction_performed(InputAction.CallbackContext obj)
    {
        OnTakeActionPerformed?.Invoke(null, EventArgs.Empty);
    }

    private static void SelectUnitMultiple_started(InputAction.CallbackContext obj)
    {
        OnSelectUnitMultipleStarted?.Invoke(null, EventArgs.Empty);
    }

    private static void SelectUnitSingle_performed(InputAction.CallbackContext obj)
    {
        OnSelectUnitSinglePerformed?.Invoke(null, EventArgs.Empty);
    }

    /// <summary>
    /// Get the state of the inclusive selection button.
    /// </summary>
    /// <returns>True while the button is pressed, otherwise False.</returns>
    public static bool IsInclusive() => unitInputActions.Unit.SelectUnitInclusive.ReadValue<float>() > 0f;

    /// <summary>
    /// Get the state of the selection button.
    /// </summary>
    /// <returns>True while the selection button is pressed, otherwise False.</returns>
    public static bool IsSelectUnitMultipleHeld() => unitInputActions.Unit.SelectUnitMultiple.ReadValue<float>() > 0f;

    /// <summary>
    /// Get the state of the selection button.
    /// </summary>
    /// <returns>True if the selection button has been released on this frame, otherwise False.</returns>
    public static bool IsSelectUnitMultipleReleased() => unitInputActions.Unit.SelectUnitMultiple.WasReleasedThisFrame();
}
