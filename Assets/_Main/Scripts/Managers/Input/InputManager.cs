using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that is used as an event dispatcher for the inputs from the player.
/// </summary>
public static class InputManager
{
    private static PlayerInputActions PlayerInputActions;

    public static event EventHandler OnSelectUnitSinglePerformed;
    public static event EventHandler OnSelectUnitMultipleStarted;
    public static event EventHandler OnTakeActionPerformed;

    static InputManager()
    {
        PlayerInputActions = new PlayerInputActions();

        PlayerInputActions.Unit.Enable();
        PlayerInputActions.Camera.Enable();
        PlayerInputActions.Unit.SelectUnitSingle.performed += SelectUnitSingle_performed;
        PlayerInputActions.Unit.SelectUnitMultiple.started += SelectUnitMultiple_started;
        PlayerInputActions.Unit.TakeAction.performed += TakeAction_performed;
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

    public static Vector3 GetCameraMovement()
    {
        float x = PlayerInputActions.Camera.Movement.ReadValue<Vector2>().x;
        float y = 0f;
        float z = PlayerInputActions.Camera.Movement.ReadValue<Vector2>().y;
        Vector3 movementDirection = new Vector3(x, y, z);

        return movementDirection;
    }

    public static float GetMouseScrollClamped()
    {
        float mouseScroll = PlayerInputActions.Camera.Zoom.ReadValue<float>();
        mouseScroll = Mathf.Clamp(mouseScroll, -1f, 1f);

        return mouseScroll;
    }

    public static float GetRotationDirection() => PlayerInputActions.Camera.Rotation.ReadValue<float>();
}
