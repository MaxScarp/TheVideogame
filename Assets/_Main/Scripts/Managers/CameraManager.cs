using System.Collections.Generic;

/// <summary>
/// Class that is used for handling all cameras of the game.
/// </summary>
public static class CameraManager
{
    public static List<CameraSystem> CameraList;

    private static CameraSystem activeCamera;

    static CameraManager()
    {
        CameraList = new List<CameraSystem>();
    }

    /// <summary>
    /// Add a camera to the CameraList.
    /// </summary>
    /// <param name="camera">The Camera to Add.</param>
    public static void AddCamera(CameraSystem camera)
    {
        camera.ActiveCamera = CameraList.Count == 0 ? true : false;
        CameraList.Add(camera);
    }

    /// <summary>
    /// Remove a Camera from the CameraList.
    /// </summary>
    /// <param name="camera">The specified Camera to be removed.</param>
    public static void RemoveCamera(CameraSystem camera)
    {
        EnableOrDisableCamera(camera, false);
        CameraList.Remove(camera);
    }

    private static void EnableOrDisableCamera(CameraSystem camera, bool isEnabled)
    {
        camera.ActiveCamera = isEnabled;
    }
}
