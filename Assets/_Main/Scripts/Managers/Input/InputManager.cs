using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that is used as an event dispatcher for the inputs from the player.
/// </summary>
public static class InputManager
{
    private static PlayerInputActions PlayerInputActions;

    //Unit Events
    public static event EventHandler OnSelectUnitSinglePerformed;
    public static event EventHandler OnSelectUnitMultipleStarted;
    public static event EventHandler OnTakeActionPerformed;

    //Camera Events
    public static event EventHandler OnCameraRotationPerformed;
    public static event EventHandler OnCameraZoomPerformed;

    static InputManager()
    {
        PlayerInputActions = new PlayerInputActions();

        //Units
        PlayerInputActions.Unit.Enable();
        PlayerInputActions.Unit.SelectUnitSingle.performed += SelectUnitSingle_performed;
        PlayerInputActions.Unit.SelectUnitMultiple.started += SelectUnitMultiple_started;
        PlayerInputActions.Unit.TakeAction.performed += TakeAction_performed;

        //Camera
        PlayerInputActions.Camera.Enable();
        PlayerInputActions.Camera.Rotation.performed += RotateCamera_performed;
        PlayerInputActions.Camera.Zoom.performed += ZoomCamera_performed;
        PlayerInputActions.Camera.SwitchCamera.performed += SwitchCamera_performed;
    }

    #region Units
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
    private static void RotateCamera_performed(InputAction.CallbackContext obj)
    {
        OnCameraRotationPerformed?.Invoke(obj, EventArgs.Empty);
    }
    private static void ZoomCamera_performed(InputAction.CallbackContext obj)
    {
        OnCameraZoomPerformed?.Invoke(obj, EventArgs.Empty);
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
    #endregion

    #region Camera
    /// <summary>
    /// Return the current active camera position.
    /// </summary>
    public static Vector2 ActiveCameraPosition => PlayerInputActions.Camera.Movement.ReadValue<Vector2>();

    private static void SwitchCamera_performed(InputAction.CallbackContext obj)
    {
        float val = obj.ReadValue<float>();
        if (CameraManager.CameraList.Count >= 2 && val != 0)
        {
            for (int i = 0; i < CameraManager.CameraList.Count; i++)
            {
                if (CameraManager.CameraList[i].ActiveCamera)
                {
                    int newActiveCamera = i + (val > 0 ? 1 : -1);

                    if (newActiveCamera < 0)
                        newActiveCamera = CameraManager.CameraList.Count - 1;
                    else if (newActiveCamera >= CameraManager.CameraList.Count)
                        newActiveCamera = 0;

                    CameraManager.CameraList[i].ActiveCamera = false;
                    CameraManager.CameraList[newActiveCamera].ActiveCamera = true;
                    break;
                }
            }
        }
    }
    #endregion
}
