using System;
using UnityEngine;

public class MoveBehaviour : BaseBehaviour
{
    public event EventHandler OnStartMoving;
    public event EventHandler OnStopMoving;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float stoppingDistance = 0.1f;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private LayerMask mousePlaneLayerMask;

    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isActive) return;

        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);

            isActive = false;
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    /// <summary>
    /// Do the action corresponding to the MoveBehaviour.
    /// </summary>
    /// <param name="targetPosition">A position on the world to be reached.</param>
    public override void TakeAction(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

        isActive = true;

        OnStartMoving?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Get the mousePlaneLayerMask.
    /// </summary>
    /// <returns>A LayerMask representing the plane on which the Unit can move.</returns>
    public LayerMask GetMousePlaneLayerMask() => mousePlaneLayerMask;
}
