using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";

    [SerializeField] private Animator animator;

    private MoveBehaviour moveBehaviour;

    private void Awake()
    {
        if (TryGetComponent(out MoveBehaviour moveBehaviour))
        {
            this.moveBehaviour = moveBehaviour;

            this.moveBehaviour.OnStartMoving += MoveBehaviour_OnStartMoving;
            this.moveBehaviour.OnStopMoving += MoveBehaviour_OnStopMoving;
        }
    }

    private void MoveBehaviour_OnStopMoving(object sender, EventArgs e)
    {
        animator.SetBool(IS_RUNNING, false);
    }

    private void MoveBehaviour_OnStartMoving(object sender, EventArgs e)
    {
        animator.SetBool(IS_RUNNING, true);
    }

    private void OnDestroy()
    {
        if (moveBehaviour != null)
        {
            moveBehaviour.OnStartMoving -= MoveBehaviour_OnStartMoving;
            moveBehaviour.OnStopMoving -= MoveBehaviour_OnStopMoving;
        }
    }
}
