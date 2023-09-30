using Cinemachine;
using System;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minFollowTargetY = 2f;
    [SerializeField] private float maxFollowTargetY = 12f;

    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;
    private bool allowRotation;

    private void Awake()
    {
        InputManager.OnAllowRotationActionStarted += InputManager_OnAllowRotationActionStarted;
        InputManager.OnAllowRotationActionPerformed += InputManager_OnAllowRotationActionPerformed;
    }

    private void InputManager_OnAllowRotationActionPerformed(object sender, EventArgs e)
    {
        allowRotation = false;
    }

    private void InputManager_OnAllowRotationActionStarted(object sender, EventArgs e)
    {
        allowRotation = true;
    }

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = transform.forward * InputManager.GetCameraMovement().z + transform.right * InputManager.GetCameraMovement().x;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        if (allowRotation)
        {
            Vector3 rotationVector = new Vector3(transform.eulerAngles.x, InputManager.GetMouseRotation(), transform.eulerAngles.z) * rotationSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(rotationVector);
            transform.rotation *= rotation;
        }
    }

    private void HandleZoom()
    {
        targetFollowOffset.y += InputManager.GetMouseScrollClamped();
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minFollowTargetY, maxFollowTargetY);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }

    private void OnDestroy()
    {
        InputManager.OnAllowRotationActionStarted -= InputManager_OnAllowRotationActionStarted;
        InputManager.OnAllowRotationActionPerformed -= InputManager_OnAllowRotationActionPerformed;
    }
}
