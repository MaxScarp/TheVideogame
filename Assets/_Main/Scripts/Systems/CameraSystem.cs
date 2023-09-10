using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    public bool ActiveCamera
    {
        get
        {
            return _activeCamera;
        }
        set
        {
            _activeCamera = value;
            GetComponentInChildren<Camera>().enabled = value;
            GetComponentInChildren<AudioListener>().enabled = value;
        }
    }

    //Horizontal motion
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 10f;

    //Zoom motion
    [SerializeField] private float stepSize = 2f;
    [SerializeField] private float zoomDampening = 7.5f;
    [SerializeField] private float minHeight = 5f;
    [SerializeField] private float maxHeight = 50f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField]
    [Range(1f, 10f)] private float zoomTilt; //Game restart required, do not edit in runtime

    //Rotation motion
    [SerializeField] private float maxRotationSpeed = 0.5f;

    //Screen edge motion
    [SerializeField]
    [Range(0f, 0.1f)] private float edgeTorelance = 0.05f;
    [SerializeField] private bool useScreenEdge = true;

    [SerializeField] private bool _activeCamera;

    //Horizontal motion
    private float speed;
    private float damping = 15f;

    //CameraContrlAction is unitInputActions.Camera
    private Transform cameraTransform;

    private Vector3 targetPosition;
    private float zoomHeight;
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    #region Unity functions
    private void Awake()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        InputManager.OnCameraRotationPerformed += InputManager_OnCameraRotationPerformed;
        InputManager.OnCameraZoomPerformed += InputManager_OnCameraZoomPerformed;
        HandleCameraInstantiation();

    }

    private void OnEnable()
    {
        lastPosition = transform.position;
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(transform);
    }

    private void LateUpdate()
    {
        if (ActiveCamera)
        {
            GetCameraMovement();

            if (useScreenEdge)
            {
                CheckMouseScreenEdge();
            }

            UpdateVelocity();
            UpdateCameraPosition();
            UpdateBasePosition();
        }
    }

    private void OnDisable()
    {
        InputManager.OnCameraRotationPerformed -= InputManager_OnCameraRotationPerformed;
        InputManager.OnCameraZoomPerformed -= InputManager_OnCameraZoomPerformed;
        CameraManager.RemoveCamera(this);
    }
    #endregion

    #region Custom functions
    private void UpdateVelocity()
    {
        horizontalVelocity = (transform.position - lastPosition) * Time.deltaTime;
        horizontalVelocity.y = 0;
        lastPosition = transform.position;
    }

    private void GetCameraMovement()
    {

        Vector3 inputVal = InputManager.ActiveCameraPosition.x * GetCameraRight()
            + InputManager.ActiveCameraPosition.y * GetCameraForward();

        inputVal = inputVal.normalized;
        if (inputVal.sqrMagnitude > 0.1f)
        {
            targetPosition += inputVal;
        }
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        return right;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += targetPosition * Time.deltaTime;
        }

        targetPosition = Vector3.zero;
    }

    private void InputManager_OnCameraRotationPerformed(object sender, EventArgs e)
    {
        InputAction.CallbackContext action = (InputAction.CallbackContext)sender;
        if (!Mouse.current.middleButton.isPressed) return;

        float value = action.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, value * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);
    }

    private void InputManager_OnCameraZoomPerformed(object sender, EventArgs e)
    {
        InputAction.CallbackContext action = (InputAction.CallbackContext)sender;
        float value = -action.ReadValue<Vector2>().y / 100;
        if (Mathf.Abs(value) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + value * stepSize;
            if (zoomHeight < minHeight)
            {
                zoomHeight = minHeight;
            }
            else if (zoomHeight > maxHeight)
            {
                zoomHeight = maxHeight;
            }
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        zoomTarget -= zoomSpeed / (zoomTilt + 3f) * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(transform);
    }

    private void CheckMouseScreenEdge()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 moveDir = Vector3.zero;

        //X pos
        if (mousePos.x < edgeTorelance * Screen.width)
        {
            moveDir += -GetCameraRight();
        }
        else if (mousePos.x > (1f - edgeTorelance) * Screen.width)
        {
            moveDir += GetCameraRight();
        }

        //Y pos
        if (mousePos.y < edgeTorelance * Screen.height)
        {
            moveDir += -GetCameraForward();
        }
        else if (mousePos.y > (1f - edgeTorelance) * Screen.height)
        {
            moveDir += GetCameraForward();
        }

        targetPosition += moveDir;
    }

    private void HandleCameraInstantiation()
    {
        CameraManager.AddCamera(this);
    }
    #endregion
}
