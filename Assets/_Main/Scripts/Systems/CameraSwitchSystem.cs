using System;
using UnityEngine;

public class CameraSwitchSystem : MonoBehaviour
{
    [SerializeField] private GameObject actualCamera;
    [SerializeField] private GameObject otherCamera;
    [SerializeField] private CameraTransitionCanvas cameraTransitionCanvas;

    private void Awake()
    {
        InputManager.OnSwitchActionPerformed += InputManager_OnSwitchActionPerformed;
    }

    private void InputManager_OnSwitchActionPerformed(object sender, EventArgs e)
    {
        ChangeCamera(actualCamera, otherCamera);

        cameraTransitionCanvas.ForwardTransition();
    }

    private void ChangeCamera(GameObject actualCamera, GameObject nextCamera)
    {
        actualCamera.SetActive(false);
        actualCamera = nextCamera;
        actualCamera.SetActive(true);
    }

    private void OnDestroy()
    {
        InputManager.OnSwitchActionPerformed -= InputManager_OnSwitchActionPerformed;
    }
}
