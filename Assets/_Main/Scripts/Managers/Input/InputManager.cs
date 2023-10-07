using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Class that is used as an event dispatcher for the inputs from the player.
/// </summary>
public static class InputManager
{
    private static PlayerInputActions PlayerInputActions;

    public static event EventHandler OnSelectUnitSinglePerformed;
    public static event EventHandler OnSelectUnitMultipleStarted;
    public static event EventHandler OnTakeActionPerformed;
    public static event EventHandler OnAllowRotationActionStarted;
    public static event EventHandler OnAllowRotationActionPerformed;
    public static event EventHandler OnSwitchActionPerformed;

    static InputManager()
    {
        PlayerInputActions = new PlayerInputActions();

        PlayerInputActions.Unit.Enable();
        PlayerInputActions.Camera.Enable();
        PlayerInputActions.Unit.SelectUnitSingle.performed += SelectUnitSingle_performed;
        PlayerInputActions.Unit.SelectUnitMultiple.started += SelectUnitMultiple_started;
        PlayerInputActions.Unit.TakeAction.performed += TakeAction_performed;
        PlayerInputActions.Camera.AllowRotation.started += AllowRotation_started;
        PlayerInputActions.Camera.AllowRotation.performed += AllowRotation_performed;
        PlayerInputActions.Camera.Switch.performed += Switch_performed;
    }

    private static void Switch_performed(CallbackContext obj)
    {
        OnSwitchActionPerformed?.Invoke(null, EventArgs.Empty);
    }

    private static void AllowRotation_performed(CallbackContext obj)
    {
        OnAllowRotationActionPerformed?.Invoke(null, EventArgs.Empty);
    }

    private static void AllowRotation_started(CallbackContext obj)
    {
        OnAllowRotationActionStarted?.Invoke(null, EventArgs.Empty);
    }

    private static void TakeAction_performed(CallbackContext obj)
    {
        OnTakeActionPerformed?.Invoke(null, EventArgs.Empty);
    }

    private static void SelectUnitMultiple_started(CallbackContext obj)
    {
        OnSelectUnitMultipleStarted?.Invoke(null, EventArgs.Empty);
    }

    private static void SelectUnitSingle_performed(CallbackContext obj)
    {
        OnSelectUnitSinglePerformed?.Invoke(null, EventArgs.Empty);
    }

    /// <summary>
    /// Get the state of the inclusive selection button.
    /// </summary>
    /// <returns>True while the button is pressed, otherwise False.</returns>
    public static bool IsInclusive() => PlayerInputActions.Unit.SelectUnitInclusive.ReadValue<float>() > 0f;

    /// <summary>
    /// Get the state of the selection button.
    /// </summary>
    /// <returns>True while the selection button is pressed, otherwise False.</returns>
    public static bool IsSelectUnitMultipleHeld() => PlayerInputActions.Unit.SelectUnitMultiple.ReadValue<float>() > 0f;

    /// <summary>
    /// Get the state of the selection button.
    /// </summary>
    /// <returns>True if the selection button has been released on this frame, otherwise False.</returns>
    public static bool IsSelectUnitMultipleReleased() => PlayerInputActions.Unit.SelectUnitMultiple.WasReleasedThisFrame();

    /// <summary>
    /// Read Values from movementButtons.
    /// </summary>
    /// <returns>A Vector3 representing a movement direction.</returns>
    public static Vector3 GetCameraMovement()
    {
        float x = PlayerInputActions.Camera.Movement.ReadValue<Vector2>().x;
        float y = 0f;
        float z = PlayerInputActions.Camera.Movement.ReadValue<Vector2>().y;
        Vector3 movementDirection = new Vector3(x, y, z);

        return movementDirection;
    }

    /// <summary>
    /// Get the value of the scroll of the wheel of the mouse.
    /// </summary>
    /// <returns>A float value representing the direction of mouse scroll.</returns>
    public static float GetMouseScrollClamped()
    {
        float mouseScroll = PlayerInputActions.Camera.Zoom.ReadValue<float>();
        mouseScroll = Mathf.Clamp(mouseScroll, -1f, 1f);

        return mouseScroll;
    }

    /// <summary>
    /// Get value of deltaMouse.
    /// </summary>
    /// <returns>A float representing the delta of the mouse movement in X axis. (-1 left, 1 right, 0 none).</returns>
    public static float GetMouseRotation()
    {
        float deltaMouseX = PlayerInputActions.Camera.MouseRotation.ReadValue<Vector2>().x;

        deltaMouseX = Mathf.Clamp(deltaMouseX / 3, -10f, 10f);
        return deltaMouseX;
    }
}
