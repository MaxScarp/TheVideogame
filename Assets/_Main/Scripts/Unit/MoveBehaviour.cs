using UnityEngine;

public class MoveBehaviour : BaseBehaviour
{
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
            isActive = false;
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public override void TakeAction(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;

        isActive = true;
    }

    public LayerMask GetMousePlaneLayerMask() => mousePlaneLayerMask;
}
