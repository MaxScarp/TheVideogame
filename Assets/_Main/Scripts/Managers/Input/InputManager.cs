using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class that is used as an event dispatcher for the inputs from the player.
/// </summary>
public static class InputManager
{
    private static UnitInputActions unitInputActions;

    private static List<CameraBehaviour> CameraList = new List<CameraBehaviour>();
    public static int CameraListCount => CameraList.Count;

    private static CameraBehaviour activeCamera;

    //Unit Events
    public static event EventHandler OnSelectUnitSinglePerformed;
    public static event EventHandler OnSelectUnitMultipleStarted;
    public static event EventHandler OnTakeActionPerformed;

    //Camera Events
    public static event EventHandler OnCameraRotationPerformed;
    public static event EventHandler OnCameraZoomPerformed;

    static InputManager()
    {
        unitInputActions = new UnitInputActions();

        //Units
        unitInputActions.Unit.Enable();
        unitInputActions.Unit.SelectUnitSingle.performed += SelectUnitSingle_performed;
        unitInputActions.Unit.SelectUnitMultiple.started += SelectUnitMultiple_started;
        unitInputActions.Unit.TakeAction.performed += TakeAction_performed;

        //Camera
        unitInputActions.Camera.Enable();
        unitInputActions.Camera.Rotation.performed += RotateCamera_performed;
        unitInputActions.Camera.Zoom.performed += ZoomCamera_performed;
        unitInputActions.Camera.SwitchCamera.performed += SwitchCamera_performed;
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

    #endregion

    #region Camera
    public static InputAction CameraMovement => unitInputActions.Camera.Movement;

    private static void SwitchCamera_performed(InputAction.CallbackContext obj)
    {
        float val = obj.ReadValue<float>();
        if (CameraList.Count >= 2 && val != 0)
        {
            for (int i = 0; i < CameraList.Count; i++)
            {
                if (CameraList[i].ActiveCamera)
                {
                    int newActiveCamera = i + (val > 0 ? 1 : -1);

                    if (newActiveCamera < 0)
                        newActiveCamera = CameraList.Count - 1;
                    else if (newActiveCamera >= CameraList.Count)
                        newActiveCamera = 0;

                    CameraList[i].ActiveCamera = false;
                    CameraList[newActiveCamera].ActiveCamera = true;
                    break;
                }
            }
        }
    }

    public static void AddCamera(CameraBehaviour camera)
    {
        camera.ActiveCamera = CameraList.Count == 0 ? true : false;
        CameraList.Add(camera);
    }

    public static void RemoveCamera(CameraBehaviour camera)
    {
        EnableOrDisableCamera(camera,false);
        CameraList.Remove(camera);
    }

    private static void EnableOrDisableCamera(CameraBehaviour camera, bool isEnabled)
    {
        camera.ActiveCamera = isEnabled;
    }


    #endregion

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
