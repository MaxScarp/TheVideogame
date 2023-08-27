using UnityEngine;
using UnityEngine.InputSystem;

public static class UtilsClass
{
    public static Vector3 GetMouseScreenPosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 0f;

        return mousePosition;
    }

    public static Vector3 GetWorldToScreenPosition(Vector3 position) => Camera.main.WorldToScreenPoint(position);
}
