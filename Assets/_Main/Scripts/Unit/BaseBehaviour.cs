using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
    protected bool isActive;

    public abstract void TakeAction(Vector3 targetPosition);
}
