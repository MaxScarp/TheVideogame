using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private const string IS_RUNNING = "IsRunning";

    [SerializeField] private Animator animator;

    public void StartRunning()
    {
        animator.SetBool(IS_RUNNING, true);
    }

    public void StopRunning()
    {
        animator.SetBool(IS_RUNNING, false);
    }
}
