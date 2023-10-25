using UnityEngine;

/// <summary>
/// Class that represents a base type of Behaviour.
/// </summary>
public abstract class BaseBehaviour : MonoBehaviour
{
    protected bool isActive;

    public abstract void TakeAction(Vector3 targetPosition, Unit unit = null);
}
